﻿using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Ogolne
{
    public class OgolneCelKontaktu : BaseEntity
    {
        [DisplayName("Wartość")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}