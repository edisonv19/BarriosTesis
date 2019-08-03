--=========================================================
-- 	Author: Edison Vidal
-- 	Date: 14/05/2019
--	Description: inserto los espacios en la tabla codigo
--=========================================================

begin tran
insert into Codigo(valor, Clave, grupo, Descripcion)
values
('1','Zona','CategoriaEspacio',N'Categoría Zona'),
('2','RadioCensal','CategoriaEspacio',N'Categoría Radio Censal');
commit