USE [DBpasajes]
GO
INSERT INTO [dbo].[viaje]
           ([DepartamentoId]
           ,[Hora]
           ,[Precio]
           ,[Bus]
           ,[Asientosdis]
           ,[Conductor])
     VALUES
           (1, '2023-04-01 08:00:00', 50, 'Bus A', 30, 'Juan Perez'),
           (2, '2023-04-01 10:00:00', 60, 'Bus B', 25, 'Maria Lopez'),
           (3, '2023-04-02 12:00:00', 70, 'Bus C', 20, 'Pedro Rodriguez'),
           (4, '2023-04-02 14:00:00', 80, 'Bus D', 15, 'Ana Torres'),
           (5, '2023-04-03 16:00:00', 90, 'Bus E', 10, 'Luis Garcia'),
           (6, '2023-04-03 18:00:00', 100, 'Bus F', 5, 'Marta Fernandez'),
           (7, '2023-04-04 20:00:00', 110, 'Bus G', 3, 'Carlos Gutierrez'),
           (8, '2023-04-05 22:00:00', 120, 'Bus H', 2, 'Sofia Ramirez')
GO
INSERT INTO [dbo].[departamento]
           ([Nombredep]
           ,[Estado])
     VALUES
           ('La Paz', 1),
           ('Santa Cruz', 1),
           ('Cochabamba', 1),
           ('Oruro', 1),
           ('Potosí', 1),
           ('Chuquisaca', 1),
           ('Tarija', 1),
           ('Beni', 1),
           ('Pando', 1)
GO
INSERT INTO [dbo].[reserva]
           ([Fecha]
           ,[Cantidad]
           ,[ViajeId]
           ,[UsuarioId])
     VALUES
           ('2023-04-01 10:00:00', 2, 1, 1),     
GO
