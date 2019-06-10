using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TO;
using DAO;

namespace BL
{
    public class BLManejadorSocios
    {
       // private DAOManejadorSocios manejadorDAO = new DAOManejadorSocios();


       // public String agregarSocioBL(BLSocioNegocio socioBL)
       // {
       //     if (!socioBL.apellido1.Equals("") && socioBL.apellido2.Equals("")
       //         || socioBL.apellido1.Equals("") && !socioBL.apellido2.Equals(""))
       //     {
       //         return "ERROR, debe especificar ambos apellidos";
       //     }

            
       //   socioBL.cedula_asociado = null;

       //   socioBL.estado_socio = "ACTIVO";


       //     return manejadorDAO.agregarSocio(generarClon(socioBL));
       // }

       // private TOSocioNegocio generarClon(BLSocioNegocio socioBL)
       // { //prototype?
       //     return new TOSocioNegocio(socioBL.cedula, socioBL.cedula_asociado, socioBL.nombre,
       //         socioBL.rol, socioBL.apellido1, socioBL.apellido2, socioBL.telHab, socioBL.telPers, 
       //         socioBL.correo, clonarDir(socioBL.direccion), socioBL.estado_socio);
       // }

       // private TODireccion clonarDir(BLDireccion dir) {
       //     return new TODireccion(dir.provincia, dir.canton, dir.distrito, dir.otras_sennas, dir.cod_direccion);
       // }


       //public List<BLSocioNegocio> cargarLista()
       // {
       //     List<BLSocioNegocio> socios = new List<BLSocioNegocio>();
       //     String[] nombres = {"Juan", "Pedro", "Carla", "Gaby", "Jorge", "Emilia",
       //     "Sofia", "Pancho", "Carmen", "Luis"};
       //     for (int i = 0; i < 10; i++)
       //     {
       //         BLSocioNegocio s = new BLSocioNegocio();
       //         s.cedula = "CED00" + i;
       //         s.nombre = nombres[i];
       //         s.correo = nombres[i] + "@gmail.com";
       //         s.telPers = i * 100 + (i * 10);
       //         socios.Add(s);
       //     }
       //     return socios;
       // }


    }
}
