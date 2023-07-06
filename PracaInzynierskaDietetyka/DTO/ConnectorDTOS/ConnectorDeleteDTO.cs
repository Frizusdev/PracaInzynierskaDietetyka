using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracaInzynierskaDietetyka.DTO.ConnectorDTOS
{
	public class ConnectorDeleteDTO
    {
        public int ID { set; get; }
        public string? User_ID { set; get; }
    }
}

