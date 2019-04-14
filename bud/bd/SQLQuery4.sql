USE [bug_bd]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[count] 
@name varchar(30)
AS
BEGIN
    SELECT count(product.products_name)FROM product WHERE product.products_name = @NAME
END;