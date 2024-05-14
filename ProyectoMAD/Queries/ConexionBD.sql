-- Crear el enlace de servidor a la otra base de datos
EXEC sp_addlinkedserver   
   @server = 'DB_Bible',  -- Nombre del enlace de servidor que deseas crear
   @srvproduct = '',  
   @provider = 'SQLNCLI',  -- Proveedor OLE DB para SQL Server
   @datasrc = 'THOMPSONLAP\SQLEXPRESS';  -- Nombre del servidor donde se encuentra la base de datos DB_Bible
