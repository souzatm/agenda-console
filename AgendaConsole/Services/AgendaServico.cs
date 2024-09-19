using AgendaConsole.Models;

namespace AgendaConsole.Services
{
    public static class AgendaService
    {
        public static void AdicionaCompromisso(List<Agenda> lista)
        {
            Console.Clear();
            Console.Write("Informe o título do compromisso: ");
            string nomeCompromisso = Console.ReadLine();
            Console.Write("Informe a data do compromisso: ");
            DateTime dataCompromisso = DateTime.Parse(Console.ReadLine());

            Agenda agenda = new Agenda(nomeCompromisso, dataCompromisso);

            lista.Add(agenda);

            Console.Clear();
            Console.WriteLine("Compromisso cadastrado na agenda com sucesso!");
            Console.ReadKey();

            Console.Clear();
            AgendaVisual.Menu();
        }

        public static void ConsultaAgenda(List<Agenda> lista)
        {
            Console.Clear();
            foreach (Agenda agenda in lista.OrderBy(c => c.DataCompromisso))
            {
                Console.WriteLine(agenda);
            }
            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();
        }

        public static void ConsultaAgendaPorData(List<Agenda> lista)
        {
            Console.Write("Informe a data a ser consultada: ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            var cpm = lista.Where(c => c.DataCompromisso == data).ToList();

            foreach(Agenda agenda in cpm)
            {
                Console.WriteLine(agenda);
            }
            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();

        }

        public static void AtualizaCompromisso(List<Agenda> lista)
        {
            Console.Clear();
            Console.Write("Informe um compromisso a ser editado: ");
            string compromisso = Console.ReadLine();

            var cpm = lista.Find(c => c.Compromisso == compromisso);

            if (cpm != null)
            {
                Console.Write("Informe a nova data do compromisso: ");
                DateTime novaData = DateTime.Parse(Console.ReadLine());
                cpm.DataCompromisso = novaData;

                Console.Clear();
                Console.WriteLine("Data do compromisso atualizada!");
                Console.ReadKey();
            }

            Console.Clear();
            AgendaVisual.Menu();
        }

        public static void DeletaCompromisso(List<Agenda> lista)
        {
            Console.Clear();
            Console.Write("Informe o título do compromisso a ser deletado: ");
            string compromisso = Console.ReadLine();

            var cpm = lista.Find(c => c.Compromisso == compromisso);

            if(cpm != null)
            {
                lista.Remove(cpm);
                Console.WriteLine("Compromisso removido com sucesso!");
                Console.ReadKey();
            }

            Console.Clear();
            AgendaVisual.Menu();
        }

    }
}
