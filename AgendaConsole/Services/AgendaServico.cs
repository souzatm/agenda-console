using AgendaConsole.Data;
using AgendaConsole.Exceptions;
using AgendaConsole.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

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
            Console.WriteLine("Compromisso cadastrado na agenda com sucesso!");
            Console.ReadKey();

            Console.Clear();
            AgendaVisual.Menu();
        }

        public static void ConsultaAgenda(List<Agenda> lista)
        {
            Console.Clear();

            if (lista.Count > 0)
            {
                foreach (Agenda agenda in lista.OrderBy(c => c.DataCompromisso))
                {
                    Console.WriteLine(agenda);
                }
            }
            else
            {
                Console.WriteLine("Nenhum compromisso na lista!");
            }


            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();
        }

        public static void ConsultaAgendaPorData(List<Agenda> lista)
        {
            Console.Clear();
            Console.Write("Informe a data a ser consultada: ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            var cpm = lista.Where(c => c.DataCompromisso == data).ToList();

            if (cpm.Count > 0)
            {
                foreach (Agenda agenda in lista)
                {
                    Console.WriteLine(agenda);
                }
            }
            else
            {
                Console.WriteLine("Nenhum compromisso encontrado na data informada!");
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

                Console.WriteLine("Data do compromisso atualizada!");
            }
            else
            {
                Console.WriteLine("Nenhum compromisso encontrado!");
                
            }
            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();
        }

        public static void DeletaCompromisso(List<Agenda> lista)
        {
            Console.Clear();
            Console.Write("Informe o título do compromisso a ser deletado: ");
            string compromisso = Console.ReadLine();

            var cpm = lista.Find(c => c.Compromisso == compromisso);

            if (cpm != null)
            {
                lista.Remove(cpm);
                Console.WriteLine("Compromisso removido com sucesso!");

            }
            else
            {
                Console.WriteLine("Nenhum compromisso encontrado!");
            }

            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();
        }

        public static void SalvarCompromisso(List<Agenda> agenda)
        {
            using (var context = new AppDbContext())
            {
                context.Compromissos.AddRange(agenda);
                context.SaveChanges();
            }
        }

        public static void ConsultaCompromisso()
        {
            using (var context = new AppDbContext())
            {
                var compromissos = context.Compromissos.ToList();

                foreach (var compromisso in compromissos)
                {
                    Console.WriteLine($"ID: {compromisso.Id}, Título: {compromisso.Compromisso}, Data: {compromisso.DataCompromisso}");
                }
            }
        }
    }
}
