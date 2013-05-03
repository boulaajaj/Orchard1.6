use [test]
go
/****** Drop all elements in the database if it exists ******/
/* Drop all non-system stored procs */
DECLARE @name VARCHAR(128)
DECLARE @SQL VARCHAR(254)
SELECT @name = (SELECT top 1 sys.schemas.name + '.' + sys.objects.name  as name FROM  sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE [type] = 'P' ORDER BY [name])

WHILE @name is not null
BEGIN
	SELECT @SQL = 'DROP PROCEDURE '  + RTRIM(@name)
	EXEC (@SQL)
	PRINT 'Dropped Procedure: ' + @name
	SELECT @name = (SELECT top 1   sys.schemas.name + '.' + sys.objects.name  as name FROM    sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE [type] = 'P'  AND sys.schemas.name + '.' + sys.objects.name > @name ORDER BY [name])	
END
GO

--this gets deleted by above code
Create PROCEDURE [dbo].[DeleteViews]
  
AS
BEGIN

	/* Drop all views */
	DECLARE @name VARCHAR(128)
	DECLARE @SQL VARCHAR(254)
	SELECT @name = (SELECT top 1 sys.schemas.name + '.' + sys.objects.name  as name FROM  sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE [type] = 'V' ORDER BY [name])
	--SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'V' AND category = 0 ORDER BY [name])
	WHILE @name IS NOT NULL
	BEGIN
		SELECT @SQL = 'DROP VIEW ' + RTRIM(@name) 
		EXEC (@SQL)
		PRINT 'Dropped View: ' + @name
		SELECT @name = (SELECT top 1   sys.schemas.name + '.' + sys.objects.name  as name FROM    sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE [type] = 'V'  AND sys.schemas.name + '.' + sys.objects.name > @name ORDER BY [name])	
		--SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'V' AND category = 0 AND [name] > @name ORDER BY [name])
	END
	
END
Go

--ERROR HERE! expect one error here [Cannot DROP VIEW 'dbo._vwItemSummary' because it is being referenced by object 'vwItemSummaryTmp'.]
exec [dbo].[DeleteViews]
--execute 2nd time to remove views that had references
exec [dbo].[DeleteViews]

 
/* Drop all Foreign Key constraints */
DECLARE @name VARCHAR(128)
DECLARE @constraint VARCHAR(254)
DECLARE @SQL VARCHAR(254)
SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)
WHILE @name is not null
BEGIN
	SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
	WHILE @constraint IS NOT NULL
	BEGIN
		SELECT @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) +'] DROP CONSTRAINT ' + RTRIM(@constraint)
		EXEC (@SQL)
		PRINT 'Dropped FK Constraint: ' + @constraint + ' on ' + @name
		SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
	END
SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)
END
GO
 
/* Drop all Primary Key constraints */
DECLARE @name VARCHAR(128)
DECLARE @constraint VARCHAR(254)
DECLARE @SQL VARCHAR(254)
SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)
WHILE @name IS NOT NULL
BEGIN
	SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
	WHILE @constraint is not null
	BEGIN
		SELECT @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) +'] DROP CONSTRAINT ' + RTRIM(@constraint)
		EXEC (@SQL)
		PRINT 'Dropped PK Constraint: ' + @constraint + ' on ' + @name
		SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
	END
SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)
END
GO
 
/* Drop all tables */
DECLARE @name VARCHAR(128)
DECLARE @SQL VARCHAR(254)
SELECT @name = (SELECT top 1 sys.schemas.name + '.' + sys.objects.name  as name FROM  sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE [type] = 'U' ORDER BY [name])
--SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'U' AND category = 0 ORDER BY [name])
WHILE @name IS NOT NULL
BEGIN
	SELECT @SQL = 'DROP TABLE ' + RTRIM(@name) 
	EXEC (@SQL)
	PRINT 'Dropped Table: ' + @name
	SELECT @name = (SELECT top 1   sys.schemas.name + '.' + sys.objects.name  as name FROM    sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE [type] = 'U'  AND sys.schemas.name + '.' + sys.objects.name > @name ORDER BY [name])	
END
GO 

/**************************  Drop all Functions **************************/
DECLARE @name VARCHAR(255) 
DECLARE @type VARCHAR(10) 
DECLARE @prefix VARCHAR(255) 
DECLARE @sql VARCHAR(255) 

--SELECT top 1000  sys.schemas.name + '.' + sys.objects.name  as name FROM  sys.objects INNER JOIN sys.schemas ON sys.objects.schema_id = sys.schemas.schema_id WHERE [type] <> 'P' ORDER BY [name]
select * from sys.objects where name like 'cur%'
DECLARE curs CURSOR FOR 
    SELECT sys.schemas.name + '.' + so.name  as name, xtype  
  FROM sysobjects s 
   join  sys.objects so on so.object_id = s.id
   JOIN sys.schemas ON so.schema_id = sys.schemas.schema_id 
    WHERE xtype IN ('U', 'P', 'FN', 'IF', 'TF', 'V', 'TR') -- Configuration point 1          
    ORDER BY s.name 

OPEN curs 
FETCH NEXT FROM curs INTO @name, @type 

WHILE @@FETCH_STATUS = 0 
BEGIN 
    -- Configuration point 2 
    SET @prefix = CASE @type  
        WHEN 'U' THEN 'DROP TABLE' 
        WHEN 'P' THEN 'DROP PROCEDURE' 
        WHEN 'FN' THEN 'DROP FUNCTION' 
        WHEN 'IF' THEN 'DROP FUNCTION' 
        WHEN 'TF' THEN 'DROP FUNCTION' 
        WHEN 'V' THEN 'DROP VIEW' 
        WHEN 'TR' THEN 'DROP TRIGGER' 
    END 

    SET @sql = @prefix + ' ' + @name 
    PRINT @sql 
    EXEC(@sql) 
    FETCH NEXT FROM curs INTO @name, @type 
END 

CLOSE curs 
DEALLOCATE curs
GO
/************************** END Drop all Functions **************************/

/***** Drop all Full-text Catalogs **************************/
DECLARE @name VARCHAR(128)
DECLARE @SQL VARCHAR(254)
SELECT @name = (SELECT top 1 name FROM sys.fulltext_catalogs)
--SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'U' AND category = 0 ORDER BY [name])
WHILE @name IS NOT NULL
BEGIN
	SELECT @SQL = 'DROP FULLTEXT CATALOG ' + RTRIM(@name) 
	EXEC (@SQL)
	PRINT 'Dropped  FULLTEXT CATALOG: ' + @name
	SELECT @name = (SELECT top 1  name FROM sys.fulltext_catalogs WHERE name > @name ORDER BY [name])	
END
GO 
/***** END - Drop all Full-text Catalogs *****/


/***** BEGIN - DROP SCHEMAs *****/
DECLARE @name VARCHAR(128), @sqlCommand NVARCHAR(1000), @Rows INT = 0, @i INT = 1;
DECLARE @t TABLE(RowID INT IDENTITY(1,1), ObjectName VARCHAR(128));
 
INSERT INTO @t(ObjectName)
SELECT s.[SCHEMA_NAME] FROM INFORMATION_SCHEMA.SCHEMATA s
WHERE s.[SCHEMA_NAME] NOT IN('dbo', 'guest', 'INFORMATION_SCHEMA', 'sys', 'db_owner', 'db_accessadmin', 'db_securityadmin', 'db_ddladmin', 'db_backupoperator', 'db_datareader', 'db_datawriter', 'db_denydatareader', 'db_denydatawriter')
 
SELECT @Rows = (SELECT COUNT(RowID) FROM @t), @i = 1;
 
WHILE (@i <= @Rows) 
BEGIN
    SELECT @sqlCommand = 'DROP SCHEMA [' + t.ObjectName + '];', @name = t.ObjectName FROM @t t WHERE RowID = @i;
    EXEC sp_executesql @sqlCommand;        
    PRINT 'Dropped SCHEMA: [' + @name + ']';    
    SET @i = @i + 1;
END
GO
/***** END - DROP SCHEMAs *****/

/***** BEGIN - DROP UDTs *****/
DECLARE @name VARCHAR(128), @sqlCommand NVARCHAR(1000), @Rows INT = 0, @i INT = 1;
DECLARE @t TABLE(RowID INT IDENTITY(1,1), ObjectName VARCHAR(128));
 
INSERT INTO @t(ObjectName)
(SELECT name From sys.types
where is_user_defined = 1)
 
SELECT @Rows = (SELECT COUNT(RowID) FROM @t), @i = 1;
 
WHILE (@i <= @Rows) 
BEGIN
    SELECT @sqlCommand = 'DROP type [' + t.ObjectName + '];', @name = t.ObjectName FROM @t t WHERE RowID = @i;
    EXEC sp_executesql @sqlCommand;        
    PRINT 'Dropped Type: [' + @name + ']';    
    SET @i = @i + 1;
END
GO
/***** END - DROP UDTs *****/


/***** BEGIN - DROP users *****/
DECLARE @name VARCHAR(128), @sqlCommand NVARCHAR(1000), @Rows INT = 0, @i INT = 1;
DECLARE @t TABLE(RowID INT IDENTITY(1,1), ObjectName VARCHAR(128));
 
INSERT INTO @t(ObjectName)
(select s.name from sys.sysusers s
        where issqlrole <> 1 and hasdbaccess <> 0 and isntname <> 1)
 
SELECT @Rows = (SELECT COUNT(RowID) FROM @t), @i = 1;
 
WHILE (@i <= @Rows) 
BEGIN
    SELECT @sqlCommand = 'sp_dropuser [' + t.ObjectName + '];', @name = t.ObjectName FROM @t t WHERE RowID = @i;
    EXEC sp_executesql @sqlCommand;  
    PRINT 'Dropped user: [' + @name + ']';    
    SET @i = @i + 1;
END
GO
/***** END - DROP users *****/

Create PROCEDURE [dbo].[DeleteRoles]
  
AS
BEGIN

	/***** BEGIN - DROP Roles *****/
	DECLARE @name VARCHAR(128), @sqlCommand NVARCHAR(1000), @Rows INT = 0, @i INT = 1;
	DECLARE @t TABLE(RowID INT IDENTITY(1,1), ObjectName VARCHAR(128));
	 
	INSERT INTO @t(ObjectName)
	(SELECT DBPRINCIPAL_1.NAME AS ROLE
	FROM         SYS.DATABASE_PRINCIPALS AS DBPRINCIPAL_1 
	WHERE     (DBPRINCIPAL_1.NAME like 'aspnet%'))
	 
	SELECT @Rows = (SELECT COUNT(RowID) FROM @t), @i = 1;
	 
	WHILE (@i <= @Rows) 
	BEGIN  
		SELECT @sqlCommand = 'Drop Role  [' + t.ObjectName + '];', @name = t.ObjectName FROM @t t WHERE RowID = @i;
		EXEC sp_executesql @sqlCommand;        
		PRINT 'Dropped role: [' + @name + ']';    
		SET @i = @i + 1;
	END
END
GO
/***** END - DROP Roles *****/
Exec [dbo].[DeleteRoles]
Exec [dbo].[DeleteRoles]


/****** Create new database structure here ******/



