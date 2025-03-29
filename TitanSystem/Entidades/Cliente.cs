using System;



namespace TitanSystem.Entidades
{
     abstract class Cliente
    {
        protected string Nome { get; set; }
        protected DateTime Nascimento { get; set; }

        
         public Cliente()
        {
        }   
    }
}
