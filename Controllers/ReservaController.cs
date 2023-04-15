using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SISRESERVAS.Data;
using SISRESERVAS.Models;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;


namespace SISRESERVAS.Controllers
{
    [Authorize]
    public class ReservaController : Controller
    {
        private readonly Context _context;
        public ReservaController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int parsedUserId;

            if (!int.TryParse(userId, out parsedUserId))
            {
                return BadRequest("El usuario no tiene un ID válido.");
            }
            List<reserva> reservas = _context.Reservas.Where(r => r.usuarioId == parsedUserId).ToList(); // Filtrar las reservas por el ID de usuario
            return View(reservas); // Enviar las reservas filtradas a la vista
        }
        public IActionResult Crear()
        {
            ViewData["IdDep"] = new SelectList(_context.Departamentos, "IdDep", "NombreDep");
            ViewData["idbus"] = new SelectList(_context.Buses, "idbus", "nombrebus");
            ViewData["Idchofer"] = new SelectList(_context.Chofer, "Idchofer", "Nombrechofer");
            return View();
        }

        [HttpPost]
        public IActionResult Create(reserva reserva)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                reserva.usuarioId = int.Parse(userId);
                _context.Reservas.Add(reserva);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Editar(int id)
        {
            var reserva = _context.Reservas.Find(id);

            if (reserva == null)
            {
                return NotFound();
            }

            ViewData["IdDep"] = new SelectList(_context.Departamentos, "IdDep", "NombreDep", reserva.departamentoIdDep);
            ViewData["idbus"] = new SelectList(_context.Buses, "idbus", "nombrebus", reserva.busidbus);
            ViewData["Idchofer"] = new SelectList(_context.Chofer, "Idchofer", "Nombrechofer", reserva.choferIdchofer);

            return View(reserva);
        }
        [HttpPost]
        public IActionResult Editar(int id, reserva reserva)
        {
            if (id != reserva.IdRes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    reserva.usuarioId = int.Parse(userId);
                    _context.Update(reserva);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!reservaExists(reserva.IdRes))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["IdDep"] = new SelectList(_context.Departamentos, "IdDep", "NombreDep", reserva.departamentoIdDep);
            ViewData["idbus"] = new SelectList(_context.Buses, "idbus", "nombrebus", reserva.busidbus);
            ViewData["Idchofer"] = new SelectList(_context.Chofer, "Idchofer", "Nombrechofer", reserva.choferIdchofer);

            return View(reserva);
        }
        public IActionResult Eliminar(int? Id)
        {
            if (Id == null)
            {

                return NotFound();
            }
            //obtener datos
            var reserva = _context.Reservas.Find(Id);
            if (reserva == null)
            {
                return NotFound();
            }
            return View(reserva);
        }
        [HttpPost]
        public IActionResult Delete(int? IdRes)
        {

            if (IdRes == null)
            {
                return NotFound();
            }
            var reserva = _context.Reservas.Find(IdRes);

            _context.Reservas.Remove(reserva);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool reservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdRes == id);
        }

    }


}
