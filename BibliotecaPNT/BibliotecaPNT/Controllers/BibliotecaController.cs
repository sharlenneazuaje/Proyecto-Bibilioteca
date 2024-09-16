using BibliotecaPNT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace BibliotecaPNT.Controllers
{
    public class BibliotecaController : Controller
    {
        Context.BibliotecaContext context = new();

        [HttpGet]
        public IActionResult Index(int? page, string titulo, string autor, int? año, Genero? genero, string prestatario)
        {
            var libros = context.libros.AsQueryable();

            if (!string.IsNullOrEmpty(titulo))
            {
                libros = libros.Where(l => l.titulo.Contains(titulo));
            }
            if (!string.IsNullOrEmpty(autor))
            {
                libros = libros.Where(l => l.autor.Contains(autor));
            }
            if (año.HasValue)
            {
                libros = libros.Where(l => l.año == año);
            }
            if (genero.HasValue)
            {
                libros = libros.Where(l => l.genero == genero);
            }
            if (!string.IsNullOrEmpty(prestatario))
            {
                libros = libros.Where(l => l.prestatario != null && l.prestatario.Contains(prestatario));
            }

            // Paginación
            int pageNumber = page ?? 1;
            int pageSize = 10;

            int totalLibros = libros.Count();
            ViewBag.PageCount = (int)Math.Ceiling((double)totalLibros / pageSize);
            ViewBag.PageNumber = pageNumber;

            var librosPaginados = libros.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new LibroSearchViewModel
            {
                titulo = titulo,
                autor = autor,
                año = año,
                genero = genero,
                prestatario = prestatario,
                libros = librosPaginados
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AltaDeLibro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AltaDeLibro(Libro libro)
        {
            context.libros.Add(libro);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult EliminarLibro(int id)
        {
            var libro = context.libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost, ActionName("Eliminar")]
        public IActionResult ConfirmarEliminar(int id)
        {
            var libro = context.libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            context.libros.Remove(libro);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditarLibro(int id)
        {
            var libro = context.libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost, ActionName("Editar")]
        public IActionResult ConfirmarEditar(int id, Libro libro)
        {
            if (id != libro.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(libro);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.libros.Any(e => e.ID == libro.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult PedirPrestado(int id)
        {
            var libro = context.libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost, ActionName("PedirPrestado")]
        public IActionResult ConfirmarPrestamo(Libro libro)
        {
            if (libro.prestatario.IsNullOrEmpty())
            {
                return BadRequest();
            }
            Debug.WriteLine(libro.ToString());
            context.Update(libro);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DevolverLibro(int id)
        {
            var libro = context.libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost, ActionName("DevolverLibro")]
        public IActionResult ConfirmarDevolucion(int id)
        {
            var libro = context.libros.Find(id);
            if (libro == null)
            {
                return NotFound();
            }

            libro.prestado = false;
            libro.prestatario = null;
            context.Update(libro);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

