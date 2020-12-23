CREATE DATABASE usuario_db;
GO

USE usuario_db;
GO

CREATE TABLE tb_status (
	id INT PRIMARY KEY NOT NULL,
	nome VARCHAR(20) NOT NULL,
	criacao Date NOT NULL
);

CREATE TABLE tb_perfil_usuario (
	id INT PRIMARY KEY NOT NULL,
	nome VARCHAR(20) NOT NULL,
	id_status INT NOT NULL,
	CONSTRAINT FK_Status FOREIGN KEY (id_status)
	REFERENCES tb_status(id)
);

CREATE TABLE tb_usuario (
	id INT IDENTITY PRIMARY KEY NOT NULL,
	nome VARCHAR(150) NOT NULL,
	data_nascimento DATE NOT NULL,
	id_perfil_usuario INT NOT NULL,
	CONSTRAINT FK_Perfil_Usuario FOREIGN KEY (id_perfil_usuario)
	REFERENCES tb_perfil_usuario(id)
);
GO


-- ############# STORED PROCEDURES # STATUS #############



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		BRUNO ARMANDO TRAGL
-- Create date: 21/12/2020
-- Description:	INCLUIR REGISTRO DE STATUS
-- =============================================
CREATE PROCEDURE proc_incluir_status
(
	@id INT,
	@nome VARCHAR(20)
)
AS
DECLARE @resultado int;
BEGIN TRAN
IF EXISTS ( SELECT * FROM tb_status WHERE nome = @nome OR id = @id OR (nome = @nome AND id = @id))
	BEGIN
		SET @resultado = -2;
	END
ELSE
	BEGIN
		INSERT INTO tb_status (id, nome, criacao)
		VALUES (@id, @nome, GETDATE());
		SET @resultado = @@ERROR;
	END

IF @resultado <> 0
	BEGIN
		ROLLBACK TRAN;
	END
ELSE
	BEGIN
		COMMIT TRAN;
	END

SELECT @resultado;
GO



-- #############



-- =============================================
-- Author:		BRUNO ARMANDO TRAGL
-- Create date: 21/12/2020
-- Description:	LISTAR REGISTROS DE STATUS
-- =============================================
CREATE PROCEDURE proc_listar_status
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM tb_status;
END
GO



-- #############



-- =============================================
-- Author:		BRUNO ARMANDO TRAGL
-- Create date: 21/12/2020
-- Description:	ALTERAR REGISTRO DE STATUS
-- =============================================
CREATE PROCEDURE proc_alterar_status
(
	@id INT,
	@nome VARCHAR(20)
)
AS
DECLARE @resultado int 
BEGIN TRAN
IF EXISTS ( SELECT * FROM tb_status WHERE id = @id )
	BEGIN
		UPDATE tb_status
		SET nome = @nome
		WHERE id = @id
		SET @resultado = @@ERROR
	END
ELSE
	BEGIN
		SET @resultado = -1
	END

IF @resultado <> 0
	BEGIN
		ROLLBACK TRAN
	END
ELSE
	BEGIN
		COMMIT TRAN
	END

SELECT @resultado
GO



-- ############# STORED PROCEDURES # PERFIL DO USUÁRIO #############



-- =============================================
-- Author:		BRUNO ARMANDO TRAGL
-- Create date: 21/12/2020
-- Description:	INCLUIR REGISTRO DE PERFIL DO USUÁRIO
-- =============================================
CREATE PROCEDURE proc_incluir_perfil_usuario
(
	@id INT,
	@nome VARCHAR(20),
	@id_status INT
)
AS
DECLARE @resultado int;
BEGIN TRAN
IF EXISTS ( SELECT * FROM tb_perfil_usuario WHERE (nome = @nome AND id_status = @id_status) OR id = @id )
	BEGIN
		SET @resultado = -2;
	END
ELSE
	BEGIN
		INSERT INTO tb_perfil_usuario (id, nome, id_status)
		VALUES (@id, @nome, @id_status);
		SET @resultado = @@ERROR;
	END

IF @resultado <> 0
	BEGIN
		ROLLBACK TRAN;
	END
ELSE
	BEGIN
		COMMIT TRAN;
	END

SELECT @resultado;
GO



-- #############



-- =============================================
-- Author:		BRUNO ARMANDO TRAGL
-- Create date: 21/12/2020
-- Description:	LISTAR REGISTROS DE PERFIL DO USUÁRIO
-- =============================================
CREATE PROCEDURE proc_listar_perfil_usuario
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM tb_perfil_usuario;
END
GO



-- #############



-- =============================================
-- Author:		BRUNO ARMANDO TRAGL
-- Create date: 21/12/2020
-- Description:	ALTERAR REGISTRO DE PERFIL DO USUÁRIO
-- =============================================
CREATE PROCEDURE proc_alterar_perfil_usuario
(
	@id INT,
	@nome VARCHAR(20),
	@id_status INT
)
AS
DECLARE @resultado int 
BEGIN TRAN
IF EXISTS ( SELECT * FROM tb_perfil_usuario WHERE id = @id )
	BEGIN
		UPDATE tb_perfil_usuario
		SET nome = @nome, id_status = @id_status
		WHERE id = @id
		SET @resultado = @@ERROR
	END
ELSE
	BEGIN
		SET @resultado = -1
	END

IF @resultado <> 0
	BEGIN
		ROLLBACK TRAN
	END
ELSE
	BEGIN
		COMMIT TRAN
	END

SELECT @resultado
GO



-- ############# STORED PROCEDURES # USUÁRIO #############



-- =============================================
-- Author:		BRUNO ARMANDO TRAGL
-- Create date: 21/12/2020
-- Description:	INCLUIR REGISTRO DE USUÁRIO
-- =============================================
CREATE PROCEDURE proc_incluir_usuario
(
	@nome VARCHAR(20),
	@data_nascimento DATE,
	@id_perfil_usuario INT
)
AS
DECLARE @resultado int;
BEGIN TRAN
IF EXISTS ( SELECT * FROM tb_usuario WHERE nome = @nome AND data_nascimento = @data_nascimento AND id_perfil_usuario = @id_perfil_usuario )
	BEGIN
		SET @resultado = -2;
	END
ELSE
	BEGIN
		INSERT INTO tb_usuario (nome, data_nascimento, id_perfil_usuario)
		VALUES (@nome, @data_nascimento, @id_perfil_usuario);
		SET @resultado = @@ERROR;
	END

IF @resultado <> 0
	BEGIN
		ROLLBACK TRAN;
	END
ELSE
	BEGIN
		COMMIT TRAN;
	END

SELECT @resultado;
GO



-- #############



-- =============================================
-- Author:		BRUNO ARMANDO TRAGL
-- Create date: 21/12/2020
-- Description:	LISTAR REGISTROS DE USUÁRIOS
-- =============================================
CREATE PROCEDURE proc_listar_usuarios
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM tb_usuario;
END
GO



-- #############



-- =============================================
-- Author:		BRUNO ARMANDO TRAGL
-- Create date: 21/12/2020
-- Description:	ALTERAR REGISTRO DE USUÁRIO
-- =============================================
CREATE PROCEDURE proc_alterar_usuario
(
	@id INT,
	@nome VARCHAR(20),
	@data_nascimento DATE,
	@id_perfil_usuario INT
)
AS
DECLARE @resultado int 
BEGIN TRAN
IF EXISTS ( SELECT * FROM tb_usuario WHERE id = @id )
	BEGIN
		UPDATE tb_usuario
		SET nome = @nome, data_nascimento = @data_nascimento, id_perfil_usuario = @id_perfil_usuario
		WHERE id = @id
		SET @resultado = @@ERROR
	END
ELSE
	BEGIN
		SET @resultado = -1
	END

IF @resultado <> 0
	BEGIN
		ROLLBACK TRAN
	END
ELSE
	BEGIN
		COMMIT TRAN
	END

SELECT @resultado
GO