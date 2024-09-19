namespace AgendaConsole.Models
{
    public class Agenda
    {
        public string? Compromisso { get; set; }
        public DateTime DataCompromisso { get; set; }

        public Agenda(string? compromisso, DateTime dataCompromisso)
        {
            Compromisso = compromisso;
            DataCompromisso = dataCompromisso;
        }

        public override string ToString()
        {
            return "Compromisso: "
                + Compromisso
                + " | Data agendada: "
                + DataCompromisso.ToString("dd-MM-yyyy");
        }
    }
}
