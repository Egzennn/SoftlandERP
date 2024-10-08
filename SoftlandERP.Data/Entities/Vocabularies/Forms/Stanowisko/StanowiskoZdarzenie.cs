﻿using System.ComponentModel;

namespace SoftlandERP.Data.Entities.Vocabularies.Forms.Stanowisko
{
    public class StanowiskoZdarzenie : BaseEntity
    {
        [DisplayName("Wartość")]
        public string? Wartosc { get; set; }

        [DisplayName("Odpowiedzialny")]
        public string? Odpowiedzialny { get; set; }
    }
}
