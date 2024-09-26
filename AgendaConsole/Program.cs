using AgendaConsole.Models;
using AgendaConsole.Services;

namespace AgendaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Agenda> lista = new List<Agenda>();
            
            AgendaVisual.Menu();

            Console.Write("Selecione a opção desejada: ");
            int item = int.Parse(Console.ReadLine());
            
            do
            {
                switch (item)
                {
                    case 1:
                        AgendaService.AdicionaCompromisso();
                        break;
                    case 2:
                        AgendaService.ConsultaAgenda();
                        break;
                    case 3:
                        AgendaService.ConsultaAgendaPorData();
                        break;
                    case 4:
                        AgendaService.AtualizaCompromisso();
                        break;
                    case 5:
                        AgendaService.DeletaCompromisso();
                        break;
                }
                item = AgendaService.ContinuarAgenda();

            } while (item != 0);

            AgendaService.EncerrarAgenda();
        }
    }
}