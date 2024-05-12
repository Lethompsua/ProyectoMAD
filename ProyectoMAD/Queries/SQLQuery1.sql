EXEC sp_addlinkedserver
    @server = 'DB_Bible', -- Nombre del enlace de servidor que deseas crear
    @srvproduct = '', -- Deja en blanco o proporciona el producto de servidor
    @provider = 'SQLNCLI', -- Proveedor OLE DB para SQL Server
    @datasrc = 'THOMPSONLAP\SQLEXPRESS'; -- Nombre del servidor donde se encuentra la base de datos DB_Bible

	SELECT * FROM sys.servers;

	
	SELECT * FROM DB_Bible.dbo.Libros;
