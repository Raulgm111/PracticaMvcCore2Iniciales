using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Data;
using PracticaMvcCore2Iniciales.Models;
using System.Diagnostics.Metrics;

namespace PracticaMvcCore2Iniciales.Repositories
{
    public class RepositoryLibros
    {
        #region PROCEDURES
//        ALTER VIEW V_GRUPO_LIBROS
//AS
//    SELECT CAST(
//        ROW_NUMBER() OVER (ORDER BY IDLIBRO) AS INT) AS POSICION
//        , ISNULL(IDLIBRO, 0) AS IDLIBRO,
//        TITULO, AUTOR, EDITORIAL, PORTADA, RESUMEN, PRECIO, IDGENERO FROM LIBROS
//    GO
//    SELECT* FROM V_GRUPO_LIBROS WHERE POSICION=7

//	    ALTER PROCEDURE SP_GRUPO_libros
//    (@POSICION INT)
//    AS
//        SELECT IDLIBRO, TITULO, AUTOR,EDITORIAL,PORTADA,RESUMEN,PRECIO,IDGENERO
//        FROM V_GRUPO_LIBROS
//        WHERE POSICION >= @POSICION AND POSICION<(@POSICION+3)
//    GO
        #endregion
        public TiendaContext context;

        public RepositoryLibros(TiendaContext context)
        {
            this.context = context;
        }

        public List<Libro> GetLibros()
        {
            var consulta = from datos in this.context.Libros
                           select datos;
            return consulta.ToList();
        }

        public List<Genero> GetGeneros()
        {
            var consulta = from datos in this.context.Generos
                           select datos;
            return consulta.ToList();
        }

        public Libro FindLibro(int idlibro)
        {
            var consulta = from datos in this.context.Libros
                           where datos.IdLibro == idlibro
                           select datos;
            return consulta.FirstOrDefault();
        }

        public async Task<Libro> PaginacionLibros(int idLibro, int posicion)
        {
            string sql = "SP_IMAGENES_ZAPATILLAS @POSICION,@IDLIBRO";

            SqlParameter paramIdProducto = new("@IDLIBRO", idLibro);
            SqlParameter paramPosicion = new("@POSICION", posicion);

            List<Libro> imagenes = await this.context.Libros.FromSqlRaw(sql, paramIdProducto, paramPosicion).ToListAsync();

            return imagenes.FirstOrDefault();
        }

        public async Task<List<Libro>> GetGrupoLibrosAsync(int posicion)
        {
            string sql = "SP_GRUPO_libros @POSICION";
            SqlParameter pamposicion =
                new SqlParameter("@POSICION", posicion);
            var consulta =
                this.context.Libros.FromSqlRaw(sql, pamposicion);
            return await consulta.ToListAsync();
        }

        public int GetNumeroEmpleados()
        {
            return this.context.Libros.Count();
        }

        public List<Libro> BuscarProductoCarrito(List<int>? idproductoCarrito)
        {
            var consulta = from datos in this.context.Libros
                           where idproductoCarrito.Contains(datos.IdLibro)
                           select datos;
            return consulta.ToList();
        }
        private int GetMaxIdPedido()
        {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Pedidos.Max(z => z.IdPedido) + 1;
            }
        }

        public void AgregarPedido(int idCliente, int cantidad, int idLibro, int idFactura)
        {
            int idPedido = GetMaxIdPedido();
            List<Libro> libros = new List<Libro>();
            Pedido pedidoGeneral = new Pedido();
            pedidoGeneral.IdPedido = idPedido;
            pedidoGeneral.IdUsuario = idCliente;
            pedidoGeneral.IdLibro = idLibro;
            pedidoGeneral.Cantidad = cantidad;
            pedidoGeneral.idFactura = idFactura;
            pedidoGeneral.Fecha = DateTime.Now;

            context.Pedidos.Add(pedidoGeneral);
            context.SaveChanges();
        }

        public List<Pedido> MostrarPedidos(int idcliente)
        {
            var consulta = from pedido in context.Pedidos
                           where pedido.IdUsuario==idcliente
                           select pedido;                           

            return consulta.ToList();

        }
    }
}
