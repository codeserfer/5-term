using System;
using System.Text;


internal class NewMailEventArgs : EventArgs {
	private readonly String m_from, m_to, m_subject;

	public NewMailEventArgs (String from, String to, String subject) {
		m_from=from;
		m_to=to;
		m_subject=subject;
	}

	public String From { get { return m_from; } }
	public String To { get { return m_to; } }
	public String Subject  { get { return m_subject; } }
}

internal class MailManager {
	public event EventHandler<NewMailEventArgs> NewMail; //Событие


	//Метод, ответственный за уведомление
	protected virtual void OnNewMail (NewMailEventArgs e) {
		//Сохранить поле делегата во временное поле для обеспечения безопасности потоков
		EventHandler<NewMailEventArgs> temp=NewMail;

		//Если есть объекты, уведомить их
		if (temp!=null)
			temp (this, e);
	}

	public void SimulateNewMail (String from, String to, String subject) {
		//Объект для хранения инфорамации, которую необходимо передать
		NewMailEventArgs e=new NewMailEventArgs (from, to, subject);
		//Уведомление о событии
		OnNewMail (e);
	}

}

internal sealed class Fax {
	public Fax (MailManager mm) {
		mm.NewMail+=FaxMsg;
	}

	private void FaxMsg (Object sender, NewMailEventArgs e) {
		Console.WriteLine ("Faxing main messager:");
		Console.WriteLine ("From={0}, To={1}, Subject={2}", e.From, e.To, e.Subject);
	}

	public void Unregister (MailManager mm) {
		mm.NewMail-=FaxMsg;
	}
}

class Q {
	static void Main () {
		MailManager a=new MailManager ();
		a.SimulateNewMail ("Oler", "Dima", "subhui");
	}
}
