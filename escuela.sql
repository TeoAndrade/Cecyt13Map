CREATE DATABASE Cecyt13Map2

USE Cecyt13Map2
--proseso de registro de usuario
CREATE TABLE Usuario(
    Cve_Usuario int PRIMARY KEY IDENTITY (1,1),
    Nom_Usuario VARCHAR(50),
    Correo VARCHAR(max),
    Contraseña VARCHAR(max)
)
--proseso de creacion de la tabla de ubicacion
CREATE TABLE Ubicacion(
    Cve_Ubicacion int PRIMARY KEY IDENTITY (100,100),
    Nom_Ubicacion VARCHAR(50)
)
--proceso de creacion de la tabla de escuela
CREATE TABLE Escuela(
    Cve_Escuela int PRIMARY KEY IDENTITY(1,1),
    Nom_Escuela VARCHAR(50),
    Piso VARCHAR(50),
    Id_Ubicacion int,
    FOREIGN KEY (Id_Ubicacion) REFERENCES Ubicacion(Cve_Ubicacion)
)
--creacion de listado de salones
CREATE PROCEDURE sp_Lista_Salones
    @EdificioID int
AS
BEGIN
	select Nom_Escuela,Piso from Escuela inner join Ubicacion on Escuela.Id_Ubicacion=Ubicacion.Cve_Ubicacion
	where @EdificioID=Cve_Ubicacion
END
GO
--creacion del procedimiento registro de usuario
CREATE PROCEDURE SP_Login(
@correo VARCHAR(max),
    @contra VARCHAR(max)
)as
BEGIN
    IF(exists(select*from Usuario where Correo=@correo and Contraseña=@contra))
        SELECT Cve_Usuario FROM Usuario WHERE Correo=@correo and Contraseña=@contra
    ELSE
        SELECT '0'
end 
--procedimientro de registro
CREATE PROCEDURE SP_Registrar(
    @correo VARCHAR(max),
    @contra VARCHAR(max),
    @nombre VARCHAR(50),
    @Registrado BIT OUTPUT,--! Datos de salida
    @mensaje VARCHAR(100) OUTPUT
)as
-- en caso de que el usuario se haya registrado anteriormente
BEGIN
    if(not exists(select*from Usuario where Correo=@correo))
    BEGIN
    INSERT INTO Usuario(Nom_Usuario,Correo,Contraseña) VALUES(@nombre,@correo,@contra)
    SET @Registrado=1
    SET @mensaje='Usuario ya registrado'
    END
    ELSE
    BEGIN
        SET @Registrado=0
        set @mensaje='correo ya existe'
    END
end
go
--insertar nombre de edificio
INSERT into Ubicacion VALUES
('Edificio Norte'),
('Edificio Sur'),
('Edificio Central'),
('Gestion Escolar'),
('Edificio Norponiente'),
('Edificio Poniente');
--insertar nombre de salones
insert into Escuela values
('Sala Siglo XXI','PB',100),
('Dibujo','PB',100),
('Actividades Deportivas','PB',100),
('Audiovisual','PB',100),
('6IV4','PB',100),
('6IV5','PB',100),
('6IV6','1',100),
('6IV7','1',100),
('6IV8','1',100),
('Laboratorio de Biologia','1',100),
('Laboratorio de Quimica','1',100),
('Laboratorio de Fisica','1',100),
('6IV9','2',100),
('6IV10','2',100),
('6IV11','2',100),
('6IV12','2',100),
('6IV13','3',100),
('6IV14','3',100),
('6IV15','3',100),
('6IV16','3',100),
('Sala A','3',100),
('Sala B','3',100),
('Sala C','3',100),
('Sala D','3',100),
('Sala E','2',100),
('Sala F','2',100),
('Sala G','2',100),
('Sala H','2',100),
('2IV1','PB',200),
('2IV2','PB',200),
('2IV3','PB',200),
('2IV4','PB',200),
('2IV5','1',200),
('2IV6','1',200),
('2IV7','1',200),
('2IV8','1',200),
('2IV9','1',200),
('2IV10','1',200),
('2IV11','2',200),
('2IV12','2',200),
('2IV13','2',200),
('2IV14','2',200),
('2IV15','2',200),
('2IV16','2',200),
('2IV17','3',200),
('2IV18','3',200),
('2IV19','3',200),
('2IV20','3',200),
('4IV1','3',200),
('4IV2','3',200),
('4IV3','PB',300),
('4IV4','PB',300),
('4IV5','PB',300),
('4IV6','PB',300),
('4IV7','1',300),
('4IV8','1',300),
('4IV9','1',300),
('4IV10','1',300),
('4IV11','1',300),
('4IV12','1',300),
('4IV13','2',300),
('4IV14','2',300),
('4IV15','2',300),
('4IV16','2',300),
('4IV17','2',300),
('4IV18','2',300),
('4IV19','3',300),
('4IV20','3',300),
('4IV21','3',300),
('6IV1','3',300),
('6IV2','3',300),
('6IV3','3',300),
('6IV17','PB',500),
('6IV18','PB',500),
('6IV19','PB',500),
('6IV20','PB',500),
('6IV21','1',600),
('6IV22','1',600);
--proceso de  bbusqueda de salones
create PROCEDURE BuscarEscuela 
	@consulta varchar(50)
AS
BEGIN
	select Cve_Escuela,CONCAT(Nom_Escuela,'- Piso:',Piso,'- Edificio',Nom_Ubicacion)[Coincidencias],Nom_Escuela,Piso,Nom_Ubicacion from Escuela 
	inner join Ubicacion on Escuela.Id_Ubicacion=Ubicacion.Cve_Ubicacion
	where CONCAT(Nom_Escuela,'-',Piso,'-',Id_Ubicacion) like '%'+@consulta+'%'
END
--proceso de editar datos de administrador
Create PROCEDURE EditarAdmin
	-- Add the parameters for the stored procedure here
	@Cve_Admin int,
	@Nombre varchar(50),
	@Apellido varchar(50),
	@Correo varchar(50),
	@Clave varchar(100),
	@Mensaje varchar (500) output,
	@Resultado int output
AS
BEGIN
--registro de un administrador
	set @Resultado=0
	if not exists(select *from Administradores where CorreoAdmi=@Correo and CveAdmin=@Cve_Admin)
	begin
		update top(1) Administradores set
		Nom_Admin=@Nombre,
		Apellido=@Apellido,
		CorreoAdmi=@Correo,
		Clave=@Clave
		where CveAdmin=@Cve_Admin
		set @Resultado=1
	end
	else
	-- en caso de que el correo que ingrese el admin , haya sido anteriormente usado
		set @Mensaje='El correo ya existe'
END
--proceso de registro admin
create PROCEDURE [dbo].[RegistrarAdmin]
	-- Add the parameters for the stored procedure here
	@Nombre varchar(50),
	@Apellido varchar(50),
	@Correo varchar(50),
	@Clave varchar(100),
	@Mensaje varchar (500) output,
	@Resultado int output
AS
BEGIN
--en caso de que el aministrador no este registrado
	set @Resultado=0
	if not exists(select *from Administradores where CorreoAdmi=@Correo)
	begin
		insert into Administradores(Nom_Admin,Apellido,CorreoAdmi,Clave)values
		(@Nombre,@Apellido,@Correo,@Clave)
		set @Resultado=SCOPE_IDENTITY()
	end
	else
		set @Mensaje='El correo ya existe'
END
--proceso de ,crear edificio
Create PROCEDURE [dbo].[CrearEdificio]
	-- Add the parameters for the stored procedure here
	@Nombre varchar(50),
	@Mensaje varchar(100) output,
	@Resultado int output
AS
BEGIN
--en caso de que no exista la ubicacion buscada
	set @Resultado=0
	if not exists(select *from Ubicacion where Nom_Ubicacion=@Nombre)
	begin
		insert into Ubicacion (Nom_Ubicacion) values
		(@Nombre)
		set @Resultado=1
	end
	else
	--se mostrara el siguiente mensaje
		set @Mensaje='El edificio ya existe'
END
--proceso de, editar datos de algun edificio
Create PROCEDURE [dbo].[EditarEdificio] 
	-- Add the parameters for the stored procedure here
	@IdEdificio int,
	@Nombre varchar(50),
	@Mensaje varchar(100) output,
	@Resultado int output
AS
BEGIN
--proceso en caso de que el edificio que quieran agregar ya exista
	set @Resultado=0
	if not exists(select *from Ubicacion where Nom_Ubicacion=@Nombre and Cve_Ubicacion!=@IdEdificio)
	begin
		update top(1) Ubicacion set
		Nom_Ubicacion=@Nombre
		where Cve_Ubicacion=@IdEdificio
		set @Resultado=1
	end
	else
		set @Mensaje='El edificio ya existe'
END
-- proceso de eliminar edifio
Create PROCEDURE [dbo].[EliminarEdificio] 
	-- Add the parameters for the stored procedure here
	@IdEdificio int,
	@Mensaje varchar(100) output,
	@Resultado int output
AS
BEGIN
--proceso de, relaciones edificio-escuela
	set @Resultado=0
	if not exists(select *from Ubicacion inner join Escuela on Escuela.Id_Ubicacion=Ubicacion.Cve_Ubicacion
	where Escuela.Cve_Escuela=@IdEdificio)
	begin
		delete top(1) from Ubicacion
		where Cve_Ubicacion=@IdEdificio
		set @Resultado=1
	end
	else
		set @Mensaje='El edificio esta relacionado a un salon'
END
--proceso de creacion de tabla de administradores
create table Administradores(
Cve_Admin int primary key identity(1,1),
Nombre varchar(50),
Apellido varchar(50),
Correo varchar(100),
Contrasena varchar(max)

)

--proceso de crear nuevo salon
CREATE PROCEDURE [dbo].[CrearSalon] 
	-- Add the parameters for the stored procedure here
	@Nombre varchar(100),
	@Piso varchar(5),
	@IdEscuela int,
	@Mensaje varchar (500) output,
	@Resultado int output
AS
BEGIN
-- proceso en caso de que el salon que se agregue ya exista
	set @Resultado=0
	if not exists(select *from Escuela where @Nombre=Nom_Escuela)
	begin
		insert into Escuela(Nom_Escuela,Piso,Id_Ubicacion) values
		(@Nombre,@Piso,@IdEscuela)
		set @Resultado=SCOPE_IDENTITY()
	end
	else
		set @Mensaje='El salon ya existe'
END
--proceso de edicion de algun salon
CREATE PROCEDURE [dbo].[EditarSalon] 
	-- Add the parameters for the stored procedure here
	@CveEscuela int,
	@Nombre varchar(100),
	@Piso varchar(5),
	@IdEscuela int,
	@Mensaje varchar (500) output,
	@Resultado int output
AS
BEGIN
--proceso en caso de que el salon que se agregue ya exista
	set @Resultado=0
	if not exists(select *from Escuela where @Nombre=Nom_Escuela and @CveEscuela!=Cve_Escuela)
	begin
		update Escuela set
		Nom_Escuela=@Nombre,
		Piso=@Piso,
		Id_Ubicacion=@IdEscuela
		where  Cve_Escuela=@CveEscuela
		set @Resultado=1
	end
	else
		set @Mensaje='El salon ya existe'
END
-- tabla de administradores
create table Administradores(
CveAdmin int primary key identity (20,20),
NombreAdmin varchar(50),
Apellido varchar(50),
Clave varchar(100),
Reestablecer bit default 1);

create PROCEDURE [dbo].[ActualizarContraseña]
	-- Add the parameters for the stored procedure here
	@Id int,
	@Nueva varchar(max)
AS
BEGIN
	update Usuario 
	set Contraseña=@Nueva
	where Cve_Usuario=@Id
END
