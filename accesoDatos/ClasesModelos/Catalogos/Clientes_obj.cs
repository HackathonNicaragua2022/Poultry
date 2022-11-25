using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesoDatos.ClasesModelos.BodegaPrincipal
{
    public class Clientes_obj
    {
        /// <summary>
        /// ingresa un nuevo cliente a la base de datos
        /// </summary>
        /// <param name="_nuevoCliente"></param>
        /// <returns></returns>
        public string nuevoCliente(Cat_Clientes _nuevoCliente)
        {
            try
            {
                
                ayosabdDataContext dc = new ayosabdDataContext();
                if (!dc.Cat_Clientes.Any(a => a.NOMBRE_CLIENTE.Equals(_nuevoCliente.NOMBRE_CLIENTE)))
                {
                    dc.Cat_Clientes.InsertOnSubmit(_nuevoCliente);
                    dc.SubmitChanges();
                    return "1";
                }
                else
                {
                    throw new Exception("Ya hay un cliente con ese nombre!!");
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Actualiza los datos del cliente por su ID
        /// </summary>
        /// <param name="clienteActualizar"></param>
        /// <returns></returns>
        public string actualizar(Cat_Clientes clienteActualizar)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_Clientes ClienteOriginal = (from _Clientes in dc.Cat_Clientes where (_Clientes.ID_CLIENTES == clienteActualizar.ID_CLIENTES) select _Clientes).FirstOrDefault();
                ClienteOriginal.NOMBRE_CLIENTE = clienteActualizar.NOMBRE_CLIENTE;
                ClienteOriginal.DIRECCION = clienteActualizar.DIRECCION;
                ClienteOriginal.TELEFONO = clienteActualizar.TELEFONO;
                ClienteOriginal.CODIGO = clienteActualizar.CODIGO;
                ClienteOriginal.ESMAYORISTA = clienteActualizar.ESMAYORISTA;
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// elimina el cliente por su ID
        /// </summary>
        /// <param name="IDCliente"></param>
        /// <returns></returns>
        public string eliminar(int IDCliente)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                Cat_Clientes eliminarCliente = (from _Cliente in dc.Cat_Clientes where (_Cliente.ID_CLIENTES == IDCliente) select _Cliente).FirstOrDefault();
                dc.Cat_Clientes.DeleteOnSubmit(eliminarCliente);
                dc.SubmitChanges();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// obtiene todos los clientes almacenado en la base de datos
        /// </summary>
        /// <returns></returns>
        public List<ClientesRel> getAll()
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<ClientesRel> result = from _Clientes in dc.Cat_Clientes
                                                 join _rutas in dc.Cat_Rutas on _Clientes.ID_Rutas equals _rutas.ID_Rutas
                                                 select new ClientesRel
                                                 {
                                                     ID_CLIENTES = _Clientes.ID_CLIENTES,
                                                     NOMBRE_CLIENTE = _Clientes.NOMBRE_CLIENTE,
                                                     DIRECCION = _Clientes.DIRECCION,
                                                     TELEFONO = _Clientes.TELEFONO,
                                                     CODIGO = _Clientes.CODIGO,
                                                     Ruta = _rutas.NOMBRE_RUTA,
                                                     ESMAYORISTA = _Clientes.ESMAYORISTA
                                                 };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<ClientesRel>();
            }
        }
        public List<ClientesRel> getClientesByRuta(int IDRuta)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<ClientesRel> result = from _Clientes in dc.Cat_Clientes
                                                 join _rutas in dc.Cat_Rutas on _Clientes.ID_Rutas equals _rutas.ID_Rutas
                                                 where (_Clientes.ID_Rutas == IDRuta)
                                                 select new ClientesRel
                                                 {
                                                     ID_CLIENTES = _Clientes.ID_CLIENTES,
                                                     NOMBRE_CLIENTE = _Clientes.NOMBRE_CLIENTE,
                                                     DIRECCION = _Clientes.DIRECCION,
                                                     TELEFONO = _Clientes.TELEFONO,
                                                     CODIGO = _Clientes.CODIGO,
                                                     Ruta = _rutas.NOMBRE_RUTA,
                                                     ESMAYORISTA = _Clientes.ESMAYORISTA
                                                 };
                return result.ToList();
            }
            catch (Exception)
            {
                return new List<ClientesRel>();
            }
        }


        /// <summary>
        /// obtiene los clientes por ID
        /// </summary>
        /// <param name="IdCliente"></param>
        /// <returns></returns>
        public Cat_Clientes getClienteByID(int IdCliente)
        {
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                return (from _cliente in dc.Cat_Clientes where (_cliente.ID_CLIENTES == IdCliente) select _cliente).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public class ClientesRel : Cat_Clientes
        {
            private string ruta;

            public string Ruta
            {
                get { return ruta; }
                set { ruta = value; }
            }
        }
        public string copiarDeVendendores()
        {
            int TotalNuevos = 0;
            int TotalVendedoresSinRutaAsignado = 0;
            try
            {
                ayosabdDataContext dc = new ayosabdDataContext();
                IQueryable<Cat_Vendedores> resultadoVendedores = from _Vendedores in dc.Cat_Vendedores select _Vendedores;
                foreach (Cat_Vendedores vendedor in resultadoVendedores)
                {
                    if (!dc.Cat_Clientes.Any(a => a.NOMBRE_CLIENTE.Equals(vendedor.NOMBRE_VENDEDOR)))
                    {
                        int IDRuta = new Rutas_obj().getIDRutaxIDVendedor(vendedor.ID_VENDEDOR);
                        if (IDRuta > 0)
                        {
                            Cat_Clientes _nuevoCliente = new Cat_Clientes();
                            _nuevoCliente.NOMBRE_CLIENTE = vendedor.NOMBRE_VENDEDOR;
                            _nuevoCliente.DIRECCION = "<Actualizar>";
                            _nuevoCliente.TELEFONO = vendedor.CEDULA;
                            _nuevoCliente.CODIGO = vendedor.CODIGO;
                            _nuevoCliente.ID_Rutas = IDRuta;
                            _nuevoCliente.ESMAYORISTA = false;
                            string result = nuevoCliente(_nuevoCliente);
                            if (!result.Equals("1")) throw new Exception("Se copiaron " + TotalNuevos + " nuevos clientes a partir de los vendedores, pero no se pudo continuar con " + vendedor.NOMBRE_VENDEDOR + ", debido al siguiente error\n" + result);
                            TotalNuevos++;
                        }
                        else
                        {
                            TotalVendedoresSinRutaAsignado++;
                        }
                    }
                }
                string MensajeSalida = "";
                if (TotalVendedoresSinRutaAsignado <= 0)
                {
                    MensajeSalida = "Se copiaron " + TotalNuevos + " nuevos clientes a partir de los vendedores";
                }
                else
                {
                    MensajeSalida = "Se copiaron " + TotalNuevos + " nuevos clientes a partir de los vendedores y " + TotalVendedoresSinRutaAsignado + " Vendedores no se pudieron copiar por que no tienen una Ruta asignada, debera crear estos Clientes de forma Manual";
                }
                return MensajeSalida;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
