using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2Iniciales.Extensions;
using PracticaMvcCore2Iniciales.Filters;
using PracticaMvcCore2Iniciales.Models;
using PracticaMvcCore2Iniciales.Repositories;

namespace PracticaMvcCore2Iniciales.Controllers
{
    public class TiendaController : Controller
    {
        private RepositoryLibros repo;

        public TiendaController(RepositoryLibros repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Libro> libro = this.repo.GetLibros();
            return View(libro);
        }

        public IActionResult Detalles(int idLibro, int? idCarrito)
        {
            if (idCarrito != null)
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("CARRITO") == null)
                {
                    carrito = new List<int>();
                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
                }
                if (carrito.Contains(idCarrito.Value) == false)
                {
                    carrito.Add(idCarrito.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);
                }
            }
            Libro libro = this.repo.FindLibro(idLibro);
            return View(libro);
        }

        public async Task<IActionResult>
            PaginarGrupoLibros(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int registros = this.repo.GetNumeroEmpleados();
            List<Libro> lirbos =
                await this.repo.GetGrupoLibrosAsync(posicion.Value);
            ViewData["REGISTROS"] = registros;
            return View(lirbos);
        }

        public IActionResult Carrito(int? idCarrito)
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            if (carrito == null)
            {
                return View();
            }
            else
            {
                if (idCarrito != null)
                {
                    carrito.Remove(idCarrito.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);
                }

                List<Libro> productos = this.repo.BuscarProductoCarrito(carrito);
                return View(productos);
            }
        }

        [HttpPost]
        [AuthorizeUsuarios]
        public IActionResult Pedidos(int idLibro)
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            int idCliente = int.Parse(HttpContext.User.FindFirst("IdUsuario").Value);
            int cantidad = 1;
            int factura = 1;
            foreach (var item in carrito)
            {
                this.repo.AgregarPedido(idCliente, cantidad, carrito[0], factura);
            }
            HttpContext.Session.Remove("CARRITO");

            return RedirectToAction("MostrarPedidos", new { idcliente = idCliente });

        }

        public IActionResult MostrarPedidos(int idcliente)
        {
            List<Pedido> pedidos = this.repo.MostrarPedidos(idcliente);
            return View(pedidos);
        }
    }
}
