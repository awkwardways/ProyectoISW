using System;
using System.Collections.Generic;

namespace PROYECTOISW.Models;

public partial class Imagene
{
    public int IdPropiedad { get; set; }

    public byte[] Imagen { get; set; } = null!;

    public int IdFoto { get; set; }

    public virtual Propiedade IdPropiedadNavigation { get; set; } = null!;
}
