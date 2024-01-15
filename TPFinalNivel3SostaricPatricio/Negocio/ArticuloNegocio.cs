using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar(string id = "")
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select A.Id,Nombre, Codigo, ImagenUrl, Precio,A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, M.Id as IdMarca, C.Id as IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = A.IdMarca and A.IdCategoria= C.Id ";
                if (id != "")
                {
                    consulta += "and A.Id = " + id;
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (String)datos.Lector["Codigo"];
                    aux.Nombre = (String)datos.Lector["Nombre"];
                    aux.Descripcion = (String)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ImagenUrl = (String)datos.Lector["ImagenUrl"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (String)datos.Lector["ImagenUrl"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (String)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (String)datos.Lector["Categoria"];

                    lista.Add(aux);
                }
               datos.cerrarConexion();  
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<Articulo> listarConSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion TipoMarca, C.Descripcion TipoCategoria, ImagenUrl, A.Precio, A.IdMarca, A.IdCategoria From ARTICULOS A, CATEGORIAS C, MARCAS M where C.Id = A.IdCategoria AND A.IdCategoria = M.Id";
                datos.setearConsulta(consulta);
                //datos.setearProcedimiento("storedListar");

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["TipoMarca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["TipoCategoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void agregar(Articulo ArticuloNuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Nombre, Codigo,Precio, Descripcion,IdMarca,IdCategoria, ImagenUrl)values(@Nombre,@Codigo,@Precio,@Descripcion,@IdMarca,@IdCategoria, @Imagen) ");
                datos.setearParametros("@Nombre", ArticuloNuevo.Nombre);
                datos.setearParametros("Codigo", ArticuloNuevo.Codigo);
                datos.setearParametros("@Precio", ArticuloNuevo.Precio);
                datos.setearParametros("@Descripcion", ArticuloNuevo.Descripcion);
                datos.setearParametros("@IdMarca", ArticuloNuevo.Marca.Id);
                datos.setearParametros("IdCategoria", ArticuloNuevo.Categoria.Id);
                datos.setearParametros("@Imagen", ArticuloNuevo.ImagenUrl);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }

        }

        public void modificar( Articulo seleccionado)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Nombre = @Nombre, Descripcion = @Descripcion,Codigo= @CodArt,ImagenUrl= @img, Precio=@Precio,IdMarca=@IdMarca,IdCategoria=@IdCategoria where Id=@Id ");
                datos.setearParametros("@Nombre", seleccionado.Nombre);
                datos.setearParametros("@Descripcion", seleccionado.Descripcion);
                datos.setearParametros("@Codigo", seleccionado.Codigo);
                datos.setearParametros("@img", seleccionado.ImagenUrl);
                datos.setearParametros("@Precio", seleccionado.Precio);
                datos.setearParametros("@IdMarca", seleccionado.Marca.Id);

                datos.setearParametros("@IdCategoria", seleccionado.Categoria.Id);

                datos.setearParametros("@Id", seleccionado.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            { datos.cerrarConexion(); }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from ARTICULOS Where Id = @Id");
                datos.setearParametros("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public void filtrarProductos(GridView dgvProductos, string campo, string criterio, string valor)
        {

            ArticuloNegocio negocioFiltro = new ArticuloNegocio();
            List<Articulo> listaProductosF = negocioFiltro.listar();
            List<Articulo> listaFiltrar;

            if (campo == "Precio")
            {
                float valorPrecio = float.Parse(valor);
                if (criterio == "Mayor a:")
                {
                    listaFiltrar = listaProductosF.FindAll(x => x.Precio > Decimal.Parse(valor));
                    dgvProductos.DataSource = null;
                    dgvProductos.DataSource = listaFiltrar;
                    dgvProductos.DataBind();
                }
                else if (criterio == "Menor a:")
                {
                    listaFiltrar = listaProductosF.FindAll(x => x.Precio < int.Parse(valor));
                    dgvProductos.DataSource = null;
                    dgvProductos.DataSource = listaFiltrar;
                    dgvProductos.DataBind();
                }
                else
                {

                    listaFiltrar = listaProductosF.FindAll(x => x.Precio == int.Parse(valor));
                    dgvProductos.DataSource = null;
                    dgvProductos.DataSource = listaFiltrar;
                    dgvProductos.DataBind();
                }

            }

            else if (campo == "Nombre")
            {
                if (criterio == "Comienza con: ")
                {
                    listaFiltrar = listaProductosF.Where(x => x.Nombre.ToUpper().StartsWith(valor.ToUpper())).ToList();
                }
                else if (criterio == "Termina con: ")
                {
                    listaFiltrar = listaProductosF.Where(x => x.Nombre.ToUpper().EndsWith(valor.ToUpper())).ToList();
                }
                else
                {
                    criterio = "Contiene: ";
                    listaFiltrar = listaProductosF.Where(x => x.Nombre.ToUpper().Contains(valor.ToUpper())).ToList();
                }
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = listaFiltrar;
                dgvProductos.DataBind();
            }
            else if (campo == "Categoria")
            {
                if (criterio == "Comienza con: ")
                {
                    listaFiltrar = listaProductosF.Where(x => x.Categoria.Descripcion.ToUpper().StartsWith(valor.ToUpper())).ToList();
                }
                else if (criterio == "Termina con: ")
                {
                    listaFiltrar = listaProductosF.Where(x => x.Categoria.Descripcion.ToUpper().EndsWith(valor.ToUpper())).ToList();
                }
                else
                {
                    criterio = "Contiene: ";
                    listaFiltrar = listaProductosF.Where(x => x.Categoria.Descripcion.ToUpper().Contains(valor.ToUpper())).ToList();
                }
                dgvProductos.DataSource = null;
                dgvProductos.DataSource = listaFiltrar;
                dgvProductos.DataBind();
            }

        }

        public List<Articulo> listarFiltroDB(string campo, string criterio, string valor)
        {
            try
            {
                List<Articulo> listaFil = new List<Articulo>();
                AccesoDatos datos = new AccesoDatos();

                string consulta = "select A.Id,Nombre, Codigo, ImagenUrl, Precio,A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, M.Id as IdMarca, C.Id as IdCategoria from ARTICULOS A, MARCAS M, CATEGORIAS C where M.Id = A.IdMarca and A.IdCategoria= C.Id and ";

                if (campo == "Precio")
                {
                    switch (criterio)
                    {
                        case "Mayor a:":
                            consulta += "Precio > " + (float.Parse(valor));
                            break;
                        case "Menor a:":
                            consulta += "Precio < " + (float.Parse(valor));
                            break;
                        default:
                            consulta += "Precio = " + (float.Parse(valor));
                            break;
                    }
                }
                else if (campo == "Categoria")
                {
                    switch (criterio)
                    {
                        case "Comienza con: ":
                            consulta += "C.Descripcion like '" + valor + "%'";
                            break;
                        case "Termina con: ":
                            consulta += "C.Descripcion like '%" + valor + "' ";
                            break;
                        default:
                            consulta += "C.Descripcion like '%" + valor + "%'";
                            break;
                    }

                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza con: ":
                            consulta += "Nombre like '" + valor + "%'";
                            break;
                        case "Termina con: ":
                            consulta += "Nombre like '%" + valor + "' ";
                            break;
                        default:
                            consulta += "Nombre like '%" + valor + "%'";
                            break;
                    }

                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (String)datos.Lector["Codigo"];
                    aux.Nombre = (String)datos.Lector["Nombre"];
                    aux.Descripcion = (String)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ImagenUrl = (String)datos.Lector["ImagenUrl"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (String)datos.Lector["ImagenUrl"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (String)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (String)datos.Lector["Categoria"];

                    listaFil.Add(aux);
                }

                return listaFil;
            }
            catch (Exception ex)
            {

                throw ex;

            }

        }
    }
}
