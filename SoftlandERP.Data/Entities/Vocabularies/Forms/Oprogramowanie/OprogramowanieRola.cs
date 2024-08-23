﻿using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Oprogramowanie
{
    public class OprogramowanieRola : BaseEntity
    {
        [DisplayName("Wartość")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}