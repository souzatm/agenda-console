using AgendaConsole.Data;
using AgendaConsole.Exceptions;
using AgendaConsole.Models;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace AgendaConsole.Services
{
    public static class AgendaService
    {
        // Adiciona compromisso a lista Agenda em memória
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

        // Consulta compromissos a lista Agenda em memória
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
                throw new AgendaException("Nenhum compromisso localizado!");
            }


            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();
        }

        // Consulta compromissos por data a lista Agenda em memória
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
                throw new AgendaException("Nenhum compromisso encontrado!");
            }

            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();

        }

        // Atualiza compromisso por nome e atualiza a data a lista Agenda em memória
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
                throw new AgendaException("Nenhum compromisso encontrado!");
                
            }
            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();
        }

        //Deleta compromisso por nome na lista Agenda em memória
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
                throw new AgendaException("Nenhum compromisso encontrado!");
            }

            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();
        }

        //Salva os compromissos adicionados a lista Agenda no DB
        public static void SalvarCompromissoDb(List<Agenda> agenda)
        {
            try
            {
                if (agenda.Count == 0)
                {
                    throw new AgendaException("Nenhum compromisso para salvar.");
                }

                using (var context = new AppDbContext())
                {
                    context.Compromissos.AddRange(agenda);
                    context.SaveChanges();
                    Console.WriteLine("Compromisso salvo com sucesso!");
                }
            }
            catch (AgendaException e)
            {
                throw new AgendaException(e.Message);
            }   
            Console.ReadKey();
        }

        //Consulta compromissos salvos no DB
        public static void ConsultaCompromissoDb()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var compromissos = context.Compromissos.ToList();

                    if (compromissos == null)
                    {
                        throw new AgendaException("Compromisso não encontrado no Database.");
                    }

                    foreach (var compromisso in compromissos.OrderBy(c => c.DataCompromisso))
                    {
                        Console.WriteLine($"ID: {compromisso.Id}, Título: {compromisso.Compromisso}, Data: {compromisso.DataCompromisso}");
                    }
                }
            }
            catch(AgendaException e) 
            {
                throw new AgendaException(e.Message);
            }
            
            Console.ReadKey();
        }

        // Remove compromissos salvos no DB
        public static void RemoverCompromissoDb()
        {
            Console.Write("Informe o compromisso a ser removido do DataBase: ");
            string compromisso = Console.ReadLine();

            try
            {
                using (var context = new AppDbContext())
                {
                    var cpm = context.Compromissos.FirstOrDefault(c => c.Compromisso == compromisso);

                    if (cpm == null)
                    {
                        throw new AgendaException("Compromisso não encontrado no Database.");
                    }

                    context.Compromissos.Remove(cpm);
                    context.SaveChanges();
                    Console.WriteLine("Compromisso removido com sucesso!");
                }
            }
            catch (AgendaException e)
            {
                throw new AgendaException(e.Message);
            }
            Console.ReadKey();
        }
    }
}
