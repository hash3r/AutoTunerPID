using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
//using System.Object;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using Wolfram.NETLink;
//using Wolfram.NETLink.UI;



/*

public class LinkTest
{
    public static void Main(String[] args)
    {

        // This launches the Mathematica kernel:
        IKernelLink ml = MathLinkFactory.CreateKernelLink();

        // Discard the initial InputNamePacket the kernel will send when launched.
        ml.WaitAndDiscardAnswer();


        // The easiest way. Send the computation as a string and get the result in a single call:
        string result = ml.EvaluateToOutputForm("2+2", 0);
        Console.WriteLine("2 + 2 = " + result);

        // Use Evaluate() instead of EvaluateToXXX() if you want to read the result as a native type
        // instead of a string.
        ml.Evaluate("2+2");
        ml.WaitForAnswer();
        int intResult = ml.GetInteger();
        Console.WriteLine("2 + 2 = " + intResult);

        ml.Print("automatic");

        MathPictureBox mpb = new MathPictureBox(); 
        mpb.MathCommand = "Plot[x, {x,0,1}]";
        mpb.CreateGraphics();
        mpb.Show();

        if (ml != null)
        {
            ml.BeginManual();
            // Here we put a result to Mathematica manually:
            ml.Put(42);
            ml.Print("Manual");
        }

        // Always Close when done:
        ml.Close();

        Console.Read();
        

    }
}
*/


/*

private void computeButton_Click(object sender, System.EventArgs e)
  {
  if (mathKernel.IsComputing) { mathKernel.Abort();}
  else {
  computation.resultBox.Text = "";
  messagesBox.Text = "";
  printBox.Text = "";
  graphicsBox.Image = null;
  computeButton.Text = "Abort";
 
  mathKernel.Compute(inputBox.Text);
  computeButton.Text = "Compute";
 
  // Populate the various boxes with results.
  resultBox.Text = (string) mathKernel.Result;
  foreach (string msg in mathKernel.Messages)
  messagesBox.Text += (msg + "\r\n");
  foreach (string p in mathKernel.PrintOutput)
  printBox.Text += p;
  if (mathKernel.Graphics.Length  0)
  graphicsBox.Image = mathKernel.Graphics[0];
  }
  }
*/


// public abstract class AutoTunePID
// {
//   //  public abstract void AutoTunePID();
// 
// }

public class PIDSettings
{
  double      maxValue,
              minValue,
              Kp,
              Ti,
              Td;

  public PIDSettings(/*external data*/)
  {
      //members = /*external data*/
  }
}


public class AutoTunerPID
{
  private double Ts, //sample time
                 m,  //process gain
                 upperArea, //upper integral
                 lowerArea, //lower integral
                 T,  //model parameter
                 L,  //model parameter
                 Kp, //PID
                 Ti, //PID
                 Td, //PID
                 b,  //set-point weight in the proportional action
                 N,  //the derivative part is made proper by adding a pole with time constant proportional to Td via this parameter (derivative filter)
                 Ms, //magnitude margin 
                 ampStep; //amplitude of the step
  private double procValue;

  private List<double> respSamples;
  private List<double> lastRespSamples;

  private   Boolean isRun;
  protected Thread  autoTuningInThread;



  public AutoTunerPID(ref object aProcValue)
  {
    //procValue = new object();
    //procValue = aProcValue;

    autoTuningInThread = new Thread(StartAutoTuning);
    autoTuningInThread.Name = "StartAutoTuning()";
    
    isRun = true;
  }

  private void IdAreas()  //Identification of a FOPDT model using the method of the areas
  {
    double firstResSam = respSamples.First(),
            lastResSam = respSamples.Last();

    m = (lastResSam - firstResSam) / ampStep;

    if (m < 0)
      respSamples.Reverse();  //may be incorrect

//  reverse undershoot and overshoot
//  {
// 
//  }

    foreach (double elem in respSamples)
      upperArea += (lastResSam - elem) * Ts;                 //upper area

    double it0 = Math.Round( upperArea / Math.Abs(m) / Ts ); //compute the index of the vector (not the value of t0)

    for (int i = 0; i <= it0; i++)
      lowerArea += (respSamples[i] - firstResSam) * Ts;  //lower area

    //compute model params
    T = Math.Exp(1) * lowerArea / Math.Abs(m);
    L = Math.Max((upperArea - Math.Exp(1) * lowerArea / Math.Abs(m)), 0);
  }

  private void PIDTuning() //synthesis of the PID parameters
  {
    double A0, A1, A2,
           B0, B1, B2,
           C0, C1, C2,
           D0, D1, D2;
    
    Ms = 1.4;  //qf

    if (Ms == 1.4)  //conservative tuning
    {
      //PI
      A0 = 0.29;  A1 = -2.7;  A2 = 3.7;
      B0 = 8.9;   B1 = -6.6;  B2 = 3.0;
      C0 = 0;     C1 = 0;     C2 = 0;
      D0 = 0.81;  D1 = 0.73;  D2 = 1.9;

      //PID
      A0 = 3.8;  A1 = -8.4;  A2 = 7.3;
      B0 = 5.2;  B1 = -2.5;  B2 = -1.4;
      C0 = 0.89; C1 = -0.37; C2 = -4.1;
      D0 = 0.4;  D1 = 0.18;  D2 = 2.8;
    }

    else //(Ms == 2)  //more aggressive tuning
    {
      //PI
      A0 = 0.78;  A1 = -4.1;  A2 = 5.7;
      B0 = 8.9;   B1 = -6.6;  B2 = 3.0;
      C0 = 0;     C1 = 0;     C2 = 0;
      D0 = 0.48;  D1 = 0.78;  D2 = -0.45;

      //PID
      A0 = 8.4;   A1 = -9.6;  A2 = 9.8;
      B0 = 3.2;   B1 = -1.5;  B2 = -0.93;
      C0 = 0.86;  C1 = -1.9;  C2 = -0.44;
      D0 = 0.22;  D1 = 0.65;  D2 = 0.051;
    }

    double a   = m * L/T;    //normalized gain
    double tau = L / (L+T);  //normalized delay
      
    Kp = A0 / a * Math.Exp( A1 * tau + A2 * Math.Pow(tau,2) );
    Ti = L * B0 * Math.Exp( B1 * tau + B2 * Math.Pow(tau,2) );
    Td = L * C0 * Math.Exp( C1 * tau + C2 * Math.Pow(tau,2) );
    b  = D0     * Math.Exp( D1 * tau + D2 * Math.Pow(tau,2) );
    //N  = 5; //??
    Console.WriteLine(Kp);
    Console.WriteLine(Ti);
    Console.WriteLine(Td);
    Console.WriteLine(b);
    Console.WriteLine(a);
    Console.WriteLine(tau);
  }

  protected void StartAutoTuning(object aProcValue /*ref double curValue, double setPoint, double step*/)
  {
    
    respSamples     = new List<double>();
    lastRespSamples = new List<double>();

    int    rsc;                   //responseSamplesCount / 10
    bool   steadyState = false;
    
    double stepSteadyThr = 0.05;  //threshold on derivative to consider the step response to a steady state
    double procValue     = (double)aProcValue;

    while (isRun)
    {
      Thread.Sleep(200);  //procValue update   
      Console.WriteLine("What?");
      respSamples.Add(procValue);  //qf

      rsc = (int)(respSamples.Count / 10);

      if (rsc > 15)  //the step response must be made at least by 150 samples
      {
        lastRespSamples.Clear();

        for (int i = respSamples.Count - rsc; i < respSamples.Count; i++)
          lastRespSamples.Add(respSamples[i]);

        //Console.WriteLine(lastRespSamples.Max() - lastRespSamples.Min() - stepSteadyThr * (respSamples.Max() - respSamples.Min()) ) ;
        double t1 = lastRespSamples.Max() - lastRespSamples.Min(); //qf
        double t2 = stepSteadyThr * (respSamples.Max() - respSamples.Min()); //qf
        double t3 = t1 -t2;

        if (lastRespSamples.Max()-lastRespSamples.Min() < stepSteadyThr*(respSamples.Max()-respSamples.Min()))
          steadyState = true;

        if (steadyState)
        {
          IdAreas();
          PIDTuning();

          isRun = false;
        }
      }
    }
      

  }
}

public class ConvPID: AutoTunerPID
{
  private PIDSettings settings;
  
  public ConvPID(ref object aProcValue): base(ref aProcValue) 
  {
    settings = new PIDSettings(/*external data*/);

    this.autoTuningInThread.Start(aProcValue); 
  }
 
}

/*

public class PIDMillW : AutoTunePID
{
    public override void AutoTunePID()
    {
        throw new NotImplementedException();
    }
}

public class PIDClasW : AutoTunePID
{
    public override void AutoTunePID()
    {
        throw new NotImplementedException();
    }
}

public class PIDTechSump1 : AutoTunePID
{
    public override void AutoTunePID()
    {
        throw new NotImplementedException();
    }
}

public class PIDProdSump2 : AutoTunePID
{
    public override void AutoTunePID()
    {
        throw new NotImplementedException();
    }
}

public class PIDProdSump2 : AutoTunePID
{
    public override void AutoTunePID()
    {
        throw new NotImplementedException();
    }
}
*/


namespace nAutoTunerPID
{
  class Program
  {
    static void Main(string[] args)
    {


      //ConvPID conv = new ConvPID(ref t /*external data*/);

 
            
      Console.WriteLine("main");
     
      Console.Read();

    }
  }
}

