using AgendaConsole.Data;
using AgendaConsole.Exceptions;
using AgendaConsole.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace AgendaConsole.Services
{
    public static class AgendaService
    {
        // Adiciona compromisso a lista Agenda em memória
        public static void AdicionaCompromisso(List<Agenda> lista)
        {
            try
            {
                Console.Clear();
                Console.Write("Informe o título do compromisso: ");
                string nomeCompromisso = Console.ReadLine();
                Console.Write("Informe a data do compromisso: ");
                DateTime dataCompromisso = DateTime.Parse(Console.ReadLine());

                Agenda agenda = new Agenda(nomeCompromisso, dataCompromisso);

                lista.Add(agenda);
                Console.WriteLine("Compromisso cadastrado na agenda com sucesso!");
            }
            catch (AgendaException e)
            {
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
                Console.ReadKey();
            }
            finally
            {
                ContinuarAgenda();
            }
        }

        // Consulta compromissos a lista Agenda em memória
        public static void ConsultaAgenda(List<Agenda> lista)
        {
            try
            {
                Console.Clear();
                if (lista.Count > 0)
                {
                    foreach (Agenda agenda in lista.OrderBy(c => c.DataCompromisso))
                    {
                        Console.WriteLine(agenda);
                    }
                }
            }
            catch (AgendaException e)
            {
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
                Console.ReadKey();
            }
            finally
            {
                ContinuarAgenda();
            }
        }

        // Consulta compromissos por data a lista Agenda em memória
        public static void ConsultaAgendaPorData(List<Agenda> lista)
        {
            try
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
            }
            catch (AgendaException e)
            {
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
            }
            finally
            {
                ContinuarAgenda();
            }
        }

        // Atualiza compromisso por nome e atualiza a data a lista Agenda em memória
        public static void AtualizaCompromisso(List<Agenda> lista)
        {
            try
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
            }
            catch (AgendaException e)
            {
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
            }
            finally
            {
                ContinuarAgenda();
            }
        }

        //Deleta compromisso por nome na lista Agenda em memória
        public static void DeletaCompromisso(List<Agenda> lista)
        {
            try
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
            }
            catch (AgendaException e)
            {
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
            }
            finally
            {
                ContinuarAgenda();
            }
        }

        //Salva os compromissos adicionados a lista Agenda no DB
        public static void SalvarCompromissoDb(List<Agenda> agenda)
        {
            try
            {
                if (!agenda.Any())
                {
                    Console.WriteLine("Nenhum compromisso para salvar.");
                    return;
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
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
            }
            finally
            {
                ContinuarAgenda();
            }
        }

        //Consulta compromissos salvos no DB
        public static void ConsultaCompromissoDb()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    var compromissos = context.Compromissos.ToList();

                    if (!compromissos.Any())
                    {
                        Console.WriteLine("Nenhum compromisso encontrado no Database.");
                        return;
                    }

                    foreach (var compromisso in compromissos.OrderBy(c => c.DataCompromisso))
                    {
                        Console.WriteLine($"ID: {compromisso.Id}, Título: {compromisso.Compromisso}, Data: {compromisso.DataCompromisso}");
                    }
                }
            }
            catch (AgendaException e)
            {
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
            }
            finally
            {
                ContinuarAgenda();
            }
        }

        // Remove compromissos salvos no DB
        public static void RemoverCompromissoDb()
        {
            try
            {
                Console.Write("Informe o compromisso a ser removido do DataBase: ");
                string compromisso = Console.ReadLine();

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
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
            }
            finally
            {
                ContinuarAgenda();
            }
        }

        public static void ContinuarAgenda()
        {
            Console.ReadKey();
            Console.Clear();
            AgendaVisual.Menu();
            Console.Write("Selecione a opção desejada: ");
            int item = int.Parse(Console.ReadLine());
        }
    }
}
