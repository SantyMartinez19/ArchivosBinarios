using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivosBinarios
{
    //Alumno: Santy Francisco Martinez Castellanos
    //No.Control: 21211989

    public class ArcchivoBinarioEmpleados
    {
        //declaracion de flujos
        BinaryWriter bw = null; // flujo salida - escritura de datos
        BinaryReader br = null; // flujo entrada = lectura de datos 

        
        //campos de la clase
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;


        public void CrearArchivo(string Archivo)
        {
            //Variable local metodo

            char resp;

            try
            {
                //creacion del flujo para escribir datos al archivo
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));
               
                //Captura de datos
                do
                {
                    Console.Clear();

                    Console.Write("Numero del Empleado: ");
                    NumEmp = Int32.Parse(Console.ReadLine());

                    Console.Write("Nombre del Empleado: ");
                    Nombre = Console.ReadLine();

                    Console.Write("Direccion del Empleado: ");
                    Direccion = Console.ReadLine();

                    Console.Write("Telefono del Empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());

                    Console.Write("Dias Trabajados del Empleado: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());

                    Console.Write("Salario Diario del Empleado: ");
                    SalarioDiario = Single.Parse(Console.ReadLine());


                    //escbire los datos al archivo

                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.Write("\n\nDeseas Almacenar otro registro (s/n)");

                    resp = char.Parse(Console.ReadLine());

                } while ((resp == 's') || (resp == 'S') );

            
            }

            catch (IOException e)
            {
                Console.WriteLine("\nError: " + e.Message);
                Console.WriteLine("\nRuta :" + e.StackTrace);
            }

            finally
            {
                if (bw != null) bw.Close(); // CIerra el flujo - escritura

                Console.WriteLine("\nPresione <enter> para terminar la Escritura de Datos y regresar al Menu.");
                Console.ReadKey();

            }

        }

        public void MostarArchivo(string Archivo)
        {
            try
            {
                //verifica si existe el archivo

                if (File.Exists(Archivo))
                {
                    //Creacion flujo para leer datos del archivo
                    br = new BinaryReader(new FileStream(Archivo, FileMode.Open, FileAccess.Read));

                    //Despliegue de datos en pantalla

                    Console.Clear();

                    do
                    {
                        //Lectura de registros mientras no llegue  a EndoFfile

                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();


                        //Muestra de datos
                        Console.WriteLine("Numero del Empleado: {0}", NumEmp);
                        Console.WriteLine("Nombre del Empleado: {0}", Nombre);
                        Console.WriteLine("Direccion del Empleado: {0}", Direccion);
                        Console.WriteLine("Telefono del Empleado: {0}", Telefono);
                        Console.WriteLine("Dias Trabajados del Empleado: {0}", DiasTrabajados);
                        Console.WriteLine("Salario Diario del Empleado: {0}", SalarioDiario);
                        Console.WriteLine("SUELDO TOTAL del Empleado: {0:C}", (DiasTrabajados * SalarioDiario));
                        Console.WriteLine("\n");
                    }while(true);

                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nEl archivo" + Archivo + "No Existe en el Disco!!");
                    Console.Write("\nPresione <enter> para Continuar...");
                    Console.ReadKey();

                }

            }
            catch(EndOfStreamException)
            {
                Console.WriteLine("\n\nFin del Listado de Empleados");
                Console.Write("\nPresione <enter> para continuar...");
                Console.ReadKey();
            }

            finally
            {
                if(br != null) br.Close(); //cierra flujo
                Console.Write("\nPresione <enter> para terminar la lectura de Datos y regresar al Menu.");
                Console.ReadKey();
            }
        }


    }
    class Program
    {
        static void Main(string[] args)
        {

            //declaracion de variables auxiliares
            string Arch = null;
            int opcion;

            //Cracion del objeto
            ArcchivoBinarioEmpleados Al = new ArcchivoBinarioEmpleados();

            //Menu de Opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n*** ARCHIVO BINARIO EMPLEADOS***");
                Console.WriteLine("1.- Creacion de un Archivo.");
                Console.WriteLine("2.- Lectura de un Archivo.");
                Console.WriteLine("3.- Salida del Programa.");
                Console.Write("\nQue opcion deseas:");

                opcion = Int16.Parse(Console.ReadLine());

                switch (opcion)
                {

                    case 1:
                        //Bloque de escritura

                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo a Crear: ");
                            Arch = Console.ReadLine();

                            //Verifica si existe el archivo

                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo Esite!!, Deseas sobreescribirlo (s/n) ?");
                                resp = Char.Parse(Console.ReadLine());
                            }

                            if ((resp == 's') || (resp == 'S'))
                                Al.CrearArchivo(Arch);
                        }

                        catch (IOException e)
                        {
                            Console.WriteLine("/nError: ", e.Message);
                            Console.WriteLine("/nRuta: ", e.StackTrace);

                        }

                        break;


                    case 2:
                        //bloque de lectura

                        try
                        {
                            //Captura nombre archivo
                            Console.Write("\nAlimenta el Nombre del Archivo que deseas Leer: ");
                            Arch = Console.ReadLine();
                            Al.MostarArchivo(Arch);
                        }

                        catch (IOException e)
                        {
                            Console.WriteLine("\nError: {0}", e.Message);
                            Console.WriteLine("\nRuta: {0}", e.StackTrace);
                        }
                        break;

                    case 3:
                        Console.Write("\nPresione <enter> para salir del Programa");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("\nEsa Opcion No Existe!!, Presione <enter> para Continuar...");
                        Console.ReadKey();
                        break;

                }


            } while (opcion != 3);
        }
    }
}
