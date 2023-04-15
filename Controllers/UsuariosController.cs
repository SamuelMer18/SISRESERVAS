using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SISRESERVAS.Data;
using SISRESERVAS.Models;
using System.Data.SqlClient;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Mail;
using System.Net;

namespace SISRESERVAS.Controllers

{
    public class UsuariosController : Controller
    {
        private readonly Context _context;
        public UsuariosController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<usuario> ListaUsuarios = _context.Usuarios;
            return View(ListaUsuarios);
        }
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if(usuario.Edad >= 18 && usuario.Contraseña.Length > 7 ) 
                { 
                  _context.Usuarios.Add(usuario);
                  _context.SaveChanges();
                  return RedirectToAction(nameof(Login));
                }
                else
                {
                  TempData["mensaje"] = "Los campos de edad o contraseña no son correctos";
                  return RedirectToAction("Crear", "Usuarios");
                }
            }
            else
            {
               TempData["mensaje"] = "Por favor Introduzca sus datos";
               return RedirectToAction("Crear", "Usuarios");
            }
        }
        public IActionResult Editar(int? Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int parsedUserId;
            int.TryParse(userId, out parsedUserId);

            if (parsedUserId == null) { return NotFound(); }

            var usuario = _context.Usuarios.Find(parsedUserId);

            if (usuario == null)
            {
                return NotFound();
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpPost]
        public IActionResult Edit(usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if(usuario.Edad >= 18 && usuario.Contraseña.Length > 7 ) 
                { 
                  _context.Usuarios.Update(usuario);
                  _context.SaveChanges();
                  TempData["mensaje"] = "Edicion exitosa";
                  return RedirectToAction("Editar","Usuarios");
                }
                else
                {
                  TempData["mensaje"] = "Los campos de edad o contraseña no son correctos";
                  return RedirectToAction("Editar", "Usuarios");
                }
            }
            else
            {
               TempData["mensaje"] = "Por favor Introduzca sus datos";
               return RedirectToAction("Editar", "Usuarios");
            }
        }
        public IActionResult Login()
        {
            ClaimsPrincipal c = HttpContext.User;
            if (c.Identity != null)
            {
                if (c.Identity.IsAuthenticated)
                    return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(usuario u)
        {
            try
            {
                using (SqlConnection con = new("Data Source=.;Initial Catalog=DBpasajes;Integrated Security=True;Encrypt=False"))
                {
                    using (SqlCommand cmd = new("sp_validar_usuario", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar).Value = u.Correo;
                        cmd.Parameters.Add("@Contraseña", System.Data.SqlDbType.VarChar).Value = u.Contraseña;
                        con.Open();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["Correo"] != null && u.Correo != null)
                            {
                                int userId = Convert.ToInt32(dr["id"]);
                                List<Claim> c = new List<Claim>()
                                {
                                    /*new Claim(ClaimTypes.NameIdentifier, u.Correo)*/
                                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),new Claim(ClaimTypes.Email, u.Correo)

                                };
                                ClaimsIdentity ci = new(c, CookieAuthenticationDefaults.AuthenticationScheme);
                                AuthenticationProperties p = new();
                                p.AllowRefresh = true;
                                p.IsPersistent = u.MantenerActivo;


                                if (!u.MantenerActivo)
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(1);
                                else
                                    p.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);

                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ci), p);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ViewBag.Error = "Credenciales incorrectas o cuenta no registrada.";
                            }
                        }
                        con.Close();
                        TempData["mensaje"] = "Credenciales no validas.";
                        return RedirectToAction("Login", "Usuarios");
                    }
                }
            }
            catch (System.Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        public IActionResult IniciarRecuperacion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Recuperar(usuario u)
        {
            try
            {
                using (SqlConnection con = new("Data Source=.;Initial Catalog=DBpasajes;Integrated Security=True;Encrypt=False"))
                {
                    using (SqlCommand cmd = new("sp_validar_correousuario", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar).Value = u.Correo;
                        con.Open();
                        var dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["Correo"] != null && u.Correo != null)
                            {
                                string correoreceptor = dr["Correo"]?.ToString() ?? "";
                                string contraseña = dr["Contraseña"]?.ToString() ?? "";
                                string emailorigen = "mailfordevelop70@gmail.com";
                                string Contraseña = "gtiurgourbslomne";
                                MailMessage mensajeemail = new MailMessage(emailorigen, correoreceptor, "Recuperacion de Contraseña", "<p>Su contraseña es la siguiente: "+contraseña+"<p>" );
                                mensajeemail.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                smtp.EnableSsl = true;
                                smtp.UseDefaultCredentials = false;
                                smtp.Port = 587;
                                smtp.Credentials = new NetworkCredential(emailorigen,Contraseña);
                                smtp.Send(mensajeemail);
                                smtp.Dispose();
                                TempData["mensaje"] = "Correo enviado correctamente";
                                return RedirectToAction("IniciarRecuperacion", "Usuarios");
                            }
                        }
                        con.Close();
                        TempData["mensaje"] = "Correo electronico no valido.";
                        return RedirectToAction("IniciarRecuperacion", "Usuarios");
                    }
                }
            }
            catch (System.Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }
    }
}
