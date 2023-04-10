using System.Collections;
using System.Web;
using System.Collections.Concurrent;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO.Ports;
using System.Reflection;
using System.Net.Mail;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Xml.Serialization;
using System.Threading.Tasks.Dataflow;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;

internal class Program
{
    private static void Main(string[] args)
    {
        string text = "New text"; 
        #region Encoding and Decoding
        var encode = Encoding.UTF8.GetBytes(text);
        Console.WriteLine(encode);
        var decode=Encoding.UTF8.GetString(encode);
        Console.WriteLine(decode);
        // File.ReadAllText(Path,encodingType)
        // File.WriteAllText(Path,encodingType)
        #endregion
        #region DateTime
        DateTime value;
        var dateTime = DateTime.TryParse("24-10-2022",out value);
        var dateTime1 = DateTime.ParseExact("12/11/2022", "dd-MMMM-yyyy", null);
        var dateTime2 = DateTime.TryParseExact("12/11/2022", "dd-MMMM-yyyy",null,DateTimeStyles.None, out value);
        #endregion
        #region dictionary
        var dic = new Dictionary<int, string>();
        dic.Add(1, "lot");
        foreach(KeyValuePair<int, string> a in dic) { //dostuff
                   }
        string tryingValue=new string(" ");
        dic.TryGetValue(1, out tryingValue);
        var key=dic.Keys;
        var valueDic = dic.Values;
        var dictConcurrent = new ConcurrentDictionary<int, string>();
        dictConcurrent.AddOrUpdate(1, "From", (updateKey, valueOld) => "First");
        dic.Remove(1);
        dic.Clear();
        #endregion
        #region Collections
        Queue queue = new Queue();
        #endregion
        #region Assembly
        Assembly assembly = Assembly.GetExecutingAssembly();
        foreach (var type in assembly.GetTypes()) {
            Console.WriteLine(type);
        }
        Console.WriteLine(typeof(int).Assembly.FullName);
        #endregion
        Mister a = new Mister();
        Type type1 = typeof(Mister);
        var prop = type1.GetProperty("Name");
        prop.SetValue(a, "Misster");
        T varia = Activator.CreateInstance(typeof(int));

    }
    public class Equal {
        public string field1;
        public bool equal(object obj) {

            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }

            var type = obj.GetType();
            if (GetType() != type) { return false; }

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            //foreach (var field in fields) {
            //    if (fields.GetValue(this) != fields.GetValue(obj)) { return false; }
            //}

            return true;
        } }
        public class Mister {
        public string Name { get; set; }
        
        }
    }
//Mail Programming
#region Mail code
public class MailCode {

    public static bool SendMail(string from, List<string> toS, string body, string subject, List<string> mailCcs, List<string> mailBccs, List<string> attachments, List<string> replyTo) {
        try
        {
            using (MailMessage mail = new MailMessage()) {
            mail.From=new MailAddress(from);
                foreach (var to in toS) {
                    mail.To.Add(to);
                }
                foreach (var reply in replyTo)
                {
                    mail.ReplyToList.Add(reply);
                }
                foreach (var cc in mailCcs) {
                    mail.CC.Add(cc);
                }
                foreach (var bcc in mailBccs)
                {
                    mail.Bcc.Add(bcc);
                }
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;
                mail.Priority = MailPriority.High;

                System.Net.Mail.Attachment attachment;
                foreach (var attach in attachments) {
                    attachment = new System.Net.Mail.Attachment(attach);
                    mail.Attachments.Add(attachment);
                }
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Port = 25;
                smtpClient.Host = "Host";
                smtpClient.Credentials = new System.Net.NetworkCredential("uid", "pwd");
                smtpClient.Send(mail);
            }
                return true;
        }
        catch
        {
            return false;
        }
    }
}
#endregion
#region Progress

public class Prog {
    public void code() { 
        //if we declare progress with var report will not work
    IProgress<int> p=new Progress<int>(progress =>{ Console.WriteLine("This is progr{0}",progress); });
        job(p);
    }
    public void job(IProgress<int> p)
    {
        int max = 100;
        for (int i = 0; i < 00; i = i * 10) { 
        p.Report(i);
        }
    }
}

#endregion
#region Serialization

public class JsonSerialization {

    public void JavaScriptSerrializerMethod() {
        var Json= "{\"Name\":\"Fibonacci Sequence\",\"Numbers\":[0, 1, 1, 2, 3, 5, 8, 13]}"; 
        /*  JavascriptSerializer jss=new JavascriptSerializer();
         *  var deserialize=jss.deserialize<Dictionary<string,object>>(json);
         *  
      */

    }
    public class JsonseriaExample { 
    public int Age { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    
    }
    public class Person
    {
        public int Age { get; set; }
        [JsonIgnore]
        public string Name { get; set; }
        public string Address { get; set; }

    }
    public void JsonConvertMethod() {
        JsonseriaExample a = new JsonseriaExample { Age = 1, Name = "Dan", Address = "My addr" };
        Person aa = new Person { Age = 1, Name = "Dan", Address = "My addr" };
        var serialize = JsonConvert.SerializeObject(a);
        var Json = "{\"Name\":\"Fibonacci Sequence\",\"Numbers\":[0, 1, 1, 2, 3, 5, 8, 13]}";
        
        dynamic ty = JObject.Parse(Json);
       
        var resolver =new CamelCasePropertyNamesContractResolver();
        JsonConvert.SerializeObject(aa, new JsonSerializerSettings { ContractResolver = resolver, NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

    
    }

}
#region XML serializer
public class xmlSerialiaer {
    public class Dog {
        [XmlIgnore]
    public string Name { get; set; }
        [XmlElement(elementName:"Ages")]
    public string Age { get; set; }
    
    }
    public void serial() {
        var xmlser = new XmlSerializer(/* some object type*/ typeof(Dog));
        Dog dog = new Dog();
        var stream = "Hello im the stream";
        //xmlser.serializa(stream,object)
        //xmlser.Serialize(stream,dog);
        
        string t = "";
        (dog)xmlser.Deserialize(new Stream("sting"));
    
    }

}
#endregion

#region TPL flow Producer Consumer

public class TPL {
    public void Flow() {

        var bufferBlock = new BufferBlock<int>(
        new DataflowBlockOptions { BoundedCapacity = 1000 });
        var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(100)).Token;

        var producertask = Task.Run(async () =>
        {
            var random = new Random();

            while (!cancellationToken.IsCancellationRequested) {
                int value = random.Next();
                await bufferBlock.SendAsync(value, cancellationToken);
            
            }
        });

        var consumerTask = Task.Run(async () =>
        {
            while (await bufferBlock.OutputAvailableAsync())
            {

                var value = bufferBlock.Receive();
                Console.Write(value + "\n");
            }

        });
        Task.WhenAll(producertask, consumerTask);

        }
    public async void DnsExample() {
        var actionBlock = new ActionBlock<string>(async hostname=> {
            IPAddress[] ipaddr = await Dns.GetHostAddressesAsync(hostname);
            Console.WriteLine(ipaddr[0]);

        });
        actionBlock.Post("google");
        actionBlock.Complete();
        await actionBlock.Completion;
    
    }
    public static int threafd { get; private set; }
    public static void Updatethread() { threafd++;
        //Thread thread = new Thread(DeThread());
    }
    public static void DeThread() {
        threafd--;
        //if (object.invokeRequired()) { object.BeginInvoke((Action)(()=>update))}
    }


    }

}

#endregion
#region Process and Thread Affinity

public class Affinity {

    public Process getProcessByName(ref string process) {
        Process process1;
        if (process != null)
        {
            process1 = Process.GetCurrentProcess();
            return process1;
        }
        else {
            process1 = Process.GetProcessesByName(process)[0];
            return process1;
        }
    
    }
    public int GetProcessAffinity(string processName = null) {
        Process process = getProcessByName(ref processName);
        int processAffinity = (int)process.ProcessorAffinity;
        Console.WriteLine("Process {0} Affinity Mask is : {1}", processName,
FormatAffinity(processAffinity));
        return processAffinity;
    
    }

    public int SetProcessAffinity(int affinity, string processName = null) {
        Process process = getProcessByName(ref processName);
        process.ProcessorAffinity = new IntPtr(affinity);
        nt processAffinity = (int)process.ProcessorAffinity;
        Console.WriteLine("Process {0} Affinity Mask is : {1}", processName,
FormatAffinity(processAffinity));
        return processAffinity;


    }

    public string FormatAffinity(int affinity) {
        return Convert.ToString(affinity, 2).PadLeft(Environment.ProcessorCount, '0');
    }

}

#endregion
#region Parallel Execution

public class parallel {

    public void TaskExample() {
        var collection = new BlockingCollection<int>(5);
        Random random = new Random();
        var producerTask = Task.Run(() =>
        {
            for (int i = 0; i < 5; i++) {
                collection.Add(i);
                Console.WriteLine(i);
                Thread.Sleep(random.Next(100, 1000));
            }
            collection.CompleteAdding();
            Console.WriteLine("Added Producer");

        });
        var consumerTask = Task.Run(() =>
        {
            foreach (var t in collection)
            {
                Thread.Sleep(random.Next(100, 1000));
                Console.Write(t);
            }
            Console.WriteLine("Consumer Completed");

        }
        );
        Task.WaitAll(producerTask, consumerTask);

        //Parallel.Invoke
        var actions = Enumerable.Range(1, 10).Select(n => new Action(() =>
        {
            Console.WriteLine("I'm task " + n);
            if ((n & 1) == 0)
                throw new Exception("Exception from task " + n);
        })).ToArray();

        Parallel.Invoke(actions);
        Console.WriteLine("Everything Completed");

        //Returning value
        Task<int> t =Task.Run(() =>
        {
            int sum = 1; for (int i = 0; i < 10; i++) { sum += i; }
            return sum;
        });
        Console.Write(t);

        //Parallel.ForEach
        List<int> a = Enumerable.Range(0, 100).ToList();
        Parallel.ForEach(a, x => { int sum=1 + x; });
        //Parllel.For
        Parallel.For(0, 100, (x) => { int paral = 0; paral = paral + x; });
        //Methods to run Task
        Task ta =new Task(() => { for (int i = 0; i < 5; i++) { Console.WriteLine("hi"); } });
        ta.Start();
        ta.Wait();
        Task tt = Task.Run(() => { for (int i = 0; i < 5; i++) { Console.WriteLine("hi"); } });
        tt.Wait();

        Task.WhenAll(ta);
        //there is wheneanyaswell

        //Exceptions inside task
        Task exe1 = Task.Run(() => { throw new Exception(); });
        Task exe2 = Task.Run(() => { throw new Exception(); });
        try
        {
            Task.WaitAll(exe1, exe2);
        }
        catch {
            Console.Write("Exception caught");
        }

        //without wait
        Task[] tasks = new Task[] { exe1,exe2};
        while (tasks.All(task => !task.IsCompleted)) ;
        foreach (var taaa in tasks) { while (taaa.IsFaulted) { Console.WriteLine("Faulted"); } }
        //with cancelleationToken
        var cancecllationToken = new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token;
        Task task12 = Task.Run(() => { }, cancellationToken: cancecllationToken);

        SynchronizationContext context = SynchronizationContext.Current;

    }

}


#endregion

#region memory management
internal class Memories : SafeHandle {
    public Memories() : base(IntPtr.Zero, true) { }
    public Memories(int length):this()
    {
        SetHandle(Marshal.AllocHGlobal(length));
    }

    public override bool IsInvalid => throw new NotImplementedException();
   

    protected override bool ReleaseHandle()
    {
        Marshal.FreeHGlobal(handle);
        return true;
    }
}
//using or finally will dispose or handle memory
#endregion

#region Garbage Collector

public class NormalGC {
    public NormalGC() { }
    ~NormalGC() { }
}
public class Implementation {
    public void Demo() {
        var t=new NormalGC();
        //During clean livve object will exist but dead will be deleted(anything out of scope)
        GC.Collect();
        //there is one kind of object called weak object which will not be counted during garbage collection will be deleted durinf GC()
        var weakReference = new WeakReference<NormalGC>(t);
        t = null;
        var i;
        if (weakReference.TryGetTarget(out i)) { 
        
        }
        //if(weakReference.IsAlive)
    }
}

#endregion

#region HttpClient
public class HttpExample {
    public void BasicOperation() { 
    
    }
    

}
#region