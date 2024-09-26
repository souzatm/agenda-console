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
        // Adiciona compromisso no DB
        public static void AdicionaCompromisso()
        {
            try
            {
                Console.Clear();
                Console.Write("Informe o título do compromisso: ");
                string nomeCompromisso = Console.ReadLine();
                Console.Write("Informe a data do compromisso: ");
                DateTime dataCompromisso = DateTime.Parse(Console.ReadLine());

                Agenda compromisso = new Agenda(nomeCompromisso, dataCompromisso);

                using (var context = new AppDbContext())
                {
                    context.Compromissos.Add(compromisso);
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
                Console.ReadKey();
            }
        }

        // Consulta compromissos no DB
        public static void ConsultaAgenda()
        {
            try
            {
                Console.Clear();
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
                Console.ReadKey();
            }
        }

        // Consulta compromissos por data no DB
        public static void ConsultaAgendaPorData()
        {
            try
            {
                Console.Clear();
                Console.Write("Informe a data a ser consultada: ");
                DateTime data = DateTime.Parse(Console.ReadLine());

                using (var context = new AppDbContext())
                {
                    var compromissos = context.Compromissos.Where(c => c.DataCompromisso == data).ToList();

                    if (!compromissos.Any())
                    {
                        Console.WriteLine("Nenhum compromisso encontrado no Database.");  
                    }

                    foreach (var compromisso in compromissos.OrderBy(c => c.DataCompromisso))
                    {
                        Console.WriteLine($"ID: {compromisso.Id}, Título: {compromisso.Compromisso}, Data: {compromisso.DataCompromisso}");
                        Console.ReadKey();
                    }
                    Console.ReadKey();
                }
            }
            catch (AgendaException e)
            {
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
            }
        }

        // Solicita compromisso por nome e atualiza a data no DB
        public static void AtualizaCompromisso()
        {
            try
            {
                Console.Clear();
                Console.Write("Informe o título do compromisso a ser atualizado: ");
                string compromisso = Console.ReadLine();

                using (var context = new AppDbContext())
                {
                    var cpm = context.Compromissos.FirstOrDefault(c => c.Compromisso == compromisso);

                    if (cpm != null)
                    {
                        Console.Write("Informe a nova data do compromisso: ");
                        DateTime novaData = DateTime.Parse(Console.ReadLine());
                        cpm.DataCompromisso = novaData;

                        context.Compromissos.Update(cpm);
                        context.SaveChanges();
                        Console.WriteLine("Data do compromisso atualizada!");
                        Console.ReadKey();
                        return;
                    }
                    Console.WriteLine("Nenhum compromisso localizado.");
                    Console.ReadKey();
                }
            }
            catch (AgendaException e)
            {
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
            }
        }

        //Deleta compromisso por nome no DB
        public static void DeletaCompromisso()
        {
            try
            {
                Console.Clear();
                Console.Write("Informe o título do compromisso a ser deletado: ");
                string compromisso = Console.ReadLine();

                using (var context = new AppDbContext())
                {
                    var cpm = context.Compromissos.FirstOrDefault(c => c.Compromisso == compromisso);

                    if (cpm != null)
                    {
                        context.Compromissos.Remove(cpm);
                        context.SaveChanges();
                        Console.WriteLine("Compromisso removido com sucesso!");
                        Console.ReadKey();
                        return;
                    }

                    Console.WriteLine("Nenhum compromisso localizado.");
                    Console.ReadKey();
                }

            }
            catch (AgendaException e)
            {
                Console.WriteLine("OCORREU UM ERRO INESPERADO: " + e.Message);
            }

        }

        public static int ContinuarAgenda()
        {
            Console.Clear();
            AgendaVisual.Menu();
            Console.Write("Selecione a opção desejada: ");
            int item = int.Parse(Console.ReadLine());
            return item;
        }

        public static void EncerrarAgenda()
        {
            Console.Clear();
            Console.WriteLine("Obrigado por usar nosso sistema!");
            Console.WriteLine("COMPROMISSOS MARCADOS: ");
            ConsultaAgenda();
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
