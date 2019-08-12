--=========================================================
-- 	Author: Edison Vidal
-- 	Date: 11/08/2019
--	Description: inserto las estaciones en la tabla codigo
--=========================================================

begin tran
insert into Codigo(valor, Clave, grupo, Descripcion)
values
('Otoño','Otoño','Estacion',N'Otoño'),
('Otoño','Invierno','Estacion',N'Invierno'),
('Otoño','Primavera','Estacion',N'Primavera'),
('Otoño','Verano','Estacion',N'Verano');
commit