using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMusicStore.Models
{
    public class Album
    {

        public virtual int AlbumID { get; set; }
        public virtual int GenreID { get; set; }
        public virtual int ArtistID { get; set; }

        //add check that text is entered
        [Required(ErrorMessage = "An Album Title is required")]
        //set string length limits - character count
        [StringLength(160)]
        //set datatype specific to string type
        [DataType(DataType.MultilineText)]
        public virtual string Title { get; set; }

        public virtual decimal Price { get; set; }
        public virtual string AlbumArtUrl { get; set; }
        //Navigational property which allows access to 
        //Genre and Artist properties
        public virtual Genre Genre { get; set; }
        public virtual Artist Artist { get; set; }
    }
}