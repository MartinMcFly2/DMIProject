using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App_Cursos.Models
{
    public class Empleados
    {
        [PrimaryKey, AutoIncrement]
        public int IDEmp { get; set; }

        [MaxLength(50)]
        public string Nombre_de_Empleado { get; set; }

        [MaxLength(50)]
        public string Dirección { get; set; }

        public double Teléfono { get; set; }

        public int Edad { get; set; }

        [MaxLength(50)]
        public string CURP { get; set; }

        [MaxLength(50)]
        public string Tipo_de_Empleado { get; set; }

        public string FullName { get { return Nombre_de_Empleado; } }

    }
    public class CursosE
    {
        [PrimaryKey, AutoIncrement]
        public int IDCso { get; set; }

        [MaxLength(50)]
        public string Nombre_del_Curso { get; set; }

        [MaxLength(50)]
        public string Tipo_de_Curso { get; set; }

        [MaxLength(50)]
        public string Descripción_del_Curso { get; set; }

        public int Horas_del_Curso { get; set; }

        public string FullCso { get { return Nombre_del_Curso; } }
    }
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        [MaxLength(30)]
        public string EmailUser { get; set; }

        [MaxLength(16)]
        public string EmailPassword { get; set; }

        [MaxLength(30)]
        public string NombreCompleto { get; set; }

        public int Edad { get; set; }

        public DateTime FechaCreacion { get; set; }
    }

    public class Seguimiento
    {
        [PrimaryKey, AutoIncrement]
        public int IDSto { get; set; }

        public int NoEmp {  get; set; }

        [MaxLength(50)]
        public string Nombre_de_Empleado_2 { get; set; }

        [MaxLength(50)]
        public string Nombre_de_Curso_2 { get; set; }

        [MaxLength(50)]
        public string Lugar_del_Curso { get; set; }

        public DateTime Fecha {  get; set; }

        public TimeSpan Hora { get; set; }

        [MaxLength(50)]
        public string Estatus { get; set; }

        public int Calificación { get; set; }
    }
}