namespace AgendaConsole.Models
{
    public static class AgendaVisual
    {
        public static void Menu()
        {
            Console.WriteLine("+----------AGENDA-----------+");
            Console.WriteLine(" 1. Adicionar compromisso");
            Console.WriteLine(" 2. Consultar compromissos");
            Console.WriteLine(" 3. Consultar data na agenda");
            Console.WriteLine(" 4. Atualizar compromisso");
            Console.WriteLine(" 5. Deletar compromisso");

            Console.WriteLine(" 0. Sair");
            Console.WriteLine();
        }
    }
}
