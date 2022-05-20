use Cecyt13Map

--*Metodo para Loguear al usuario
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
GO

--*Metodo para Registrar al usuario
CREATE PROCEDURE SP_Registrar(
    @correo VARCHAR(max),
    @contra VARCHAR(max),
    @nombre VARCHAR(50),
    @Registrado BIT OUTPUT,--! Datos de salida
    @mensaje VARCHAR(100) OUTPUT
)as
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
        set @mensaje='El correo ya existe'
    END
end

DECLARE @registrado BIT, @mensaje VARCHAR(100)--! Declaracion de variables
EXECUTE SP_Registrar 'ivantovar26.hist@gmail.com','4F04CEA068C08E302E4F4C71AFCCCAF88831BAB819E8EB07F1F50E949C59D13A','Hector',@registrado output,@mensaje output

SELECT @registrado
SELECT @mensaje

SELECT *from Usuario

EXECUTE SP_Login 'ivantovar26.hist@gmail.com','4F04CEA068C08E302E4F4C71AFCCCAF88831BAB819E8EB07F1F50E949C59D13A'
