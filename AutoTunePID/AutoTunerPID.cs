using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Object;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
//using Wolfram.NETLink;
//using Wolfram.NETLink.UI;




/*

//////////////////////////////////////////////////////////////////////////////////////
//
//   .NET/Link source code (c) 2003, Wolfram Research, Inc. All rights reserved.
//
//   Use is governed by the terms of the .NET/Link license agreement.
//
//   Author: Todd Gayley
//
//////////////////////////////////////////////////////////////////////////////////////





/// A PictureBox subclass intended for displaying images of <iMathematica</i graphics and typeset expressions.
/// This class can be used from <iMathematica</i or .NET code. Use it like any other PictureBox, and simply set the
/// MathCommand</see property to have the resulting image displayed in the box.
/// 
/// To have the MathPictureBox display the result of a computation, set the MathCommand property to a string of
/// <iMathematica</i code (for example, "Plot[x, {x,0,1}]"). Alternatively, you can directly assign an Image to
/// display using the inherited <see cref="PictureBox.Image"Image</see property. In that case, no computation
/// is done in <iMathematica</i to generate the image. You would choose that technique if you were drawing into
/// an offscreen image from <iMathematica</i code and then placing that Image into the MathPictureBox.

public class MathPictureBox : PictureBox
{
    private IKernelLink ml;
    private bool usesFE;
    private string mathCommand;
    private string pictureType;

    /// Initializes a new instance of the MathPictureBox class.
    /// This is the constructor typically called from <iMathematica</i code. It sets the link to use to be the one back
    /// to <iMathematica</i (given by <see cref="StdLink.Link"/). You can also use the <see cref="Link"/ property
    /// to set the link later.
 
    public MathPictureBox()
    {

        // These SetStyle calls enable double-buffered drawing, although this would only be relevant
        // if you were drawing directly onto the surface in the Paint event handler, which is not a typical
        // use for this class.
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);

        SizeMode = PictureBoxSizeMode.CenterImage;
        Link = StdLink.Link;
        UseFrontEnd = true;
        PictureType = "Automatic";
    }

    /// Initializes a new instance of the MathPictureBox class.
    /// <param name="ml"The link to use for all computations.
        
    public MathPictureBox(IKernelLink ml)
    {
        Link = ml;
    }

    /// Sets or gets the link that will be used for computations.

    public IKernelLink Link
    {
        get { return ml; }
        set { ml = value; }
    }


    /// Sets the type of picture that the box should display.
    /// The legal values are "Automatic", "GIF", "JPEG", "Metafile", "StandardForm", and "TraditionalForm".
    /// The values "Automatic", "GIF", "JPEG", and "Metafile" specify a graphics format. If you use one of these,
    /// then the <see cref="MathCommand"/ property should be something that evaluates to a Graphics expression (or Graphics3D,
    /// SurfaceGraphics, etc.) It must <boldevaluate</bold to a Graphics expression, not merely produce a plot
    /// as a side effect. A common error is to end the code with a semicolon, which causes the expression to
    /// evaluate to Null, not the intended Graphics:
    /// <code
    /// // BAD
    /// myMathPictureBox.MathCommand = "Plot[x, {x,0,1}];";
    ///     
    /// // GOOD
    /// myMathPictureBox.MathCommand = "Plot[x, {x,0,1}]";
    /// </code
    /// The "Automatic" setting is the default, which assumes you are trying to display graphics, not typeset
    /// expressions, and it will automatically select the best graphics format. If you want the graphic to
    /// dynamically resize as the MathPictureBox is being resized, you should use the "Metafile" format.
    /// <para
    /// The values "StandardForm" or "TraditionalForm" indicate that you want the result of the MathCommand to
    /// be typeset in that format and displayed. These settings are not used if you want a <iMathematica</i plot or other
    /// graphic.
    /// </para
    /// </remarks
    /// <seealso cref="MathCommand"/
    /// <seealso cref="UseFrontEnd"/
    /// 
    public string PictureType
    {
        get { return pictureType; }
        set
        {
            // Fix case if user got it wrong. Also set PictureBoxSizeMode.
            string lowerCasePictType = value.ToLower();
            switch (lowerCasePictType)
            {
                case "gif":
                    pictureType = "GIF";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case "jpeg":
                    pictureType = "JPEG";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case "metafile":
                    pictureType = "Metafile";
                    SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case "standardform":
                    pictureType = "StandardForm";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case "traditionalform":
                    pictureType = "TraditionalForm";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                default:
                    // Automatic, and fall-through for bogus values.
                    pictureType = "Automatic";
                    SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
            }
        }
    }


    /// <summary
    /// Specifies whether to use the services of the <iMathematica</i notebook front end in rendering the image to display.
    /// </summary
    /// <remarks
    /// Using the front end for rendering services will result in better quality graphics, and it will allow expressions
    /// in plot labels and the like to be typeset. The default is true.
    /// <para
    /// If the front end is used, then depending on various circumstances you might see a special "Mathematica Server"
    /// instance of the front end appear in the Windows taskbar. This separate instance of the front end is used only for
    /// rendering services in the background. It has no user interface and cannot be brought the foreground.
    /// It is completely managed for you by .NET/Link.
    /// </para
    /// <para
    /// This setting is ignored if the <see cref="PictureType"/ property is set to "StandardForm" or "TraditionalForm",
    /// as typeset output always requires the services of the front end.
    /// </para
    /// </remarks
    /// <seealso cref="MathCommand"/
    /// <seealso cref="PictureType"/
    /// 
    public bool UseFrontEnd
    {
        get { return usesFE; }
        set { usesFE = value; }
    }


    /// Specifies the <iMathematica</i command that is used to generate the image to display.

    /// For graphics output, this will typically be a plotting command, such as "Plot[x,{x,0,1}]". For
    /// typeset output (i.e., the <see cref="PictureType"/ property is set to "StandardForm" or "TraditionalForm"),
    /// any expression can be given; its result will be typeset and displayed. Note that it is the <iresult</i
    /// of the expression that is displayed, so do not make the mistake of ending the expression with a
    /// semicolon, as this will make the expression evaluate to Null.
    /// <para
    /// You might find it more convenient to define the command in <iMathematica</i as a function and then
    /// specify only the function call as the MathCommand. For example, when using this class from
    /// a <iMathematica</i program, you might do this:
    /// <code
    ///     plotFunc[] := Plot[...complex plot command...];
    ///     myMathPictureBox@MathCommand = "plotFunc[]";

    public string MathCommand
    {
        get { return mathCommand; }
        set
        {
            mathCommand = value;
            if (Link == null)
            {
                Image = null;
            }
            else
            {
                if (Link == StdLink.Link)
                    StdLink.RequestTransaction();
                if (PictureType == "Automatic" || PictureType == "GIF" || PictureType == "JPEG" || PictureType == "Metafile")
                {
                    bool oldUseFEValue = Link.UseFrontEnd;
                    Link.UseFrontEnd = UseFrontEnd;
                    string oldGraphicsFmt = Link.GraphicsFormat;
                    Link.GraphicsFormat = PictureType;
                    // Graphics rendered by Mathematica often spill out of their bounding box a bit, so we'll compensate
                    // a bit by subtracting from the true width and height of the hosting component.
                    Image im = Link.EvaluateToImage(mathCommand, Width - 4, Height - 4);
                    Image = im;
                    Link.UseFrontEnd = oldUseFEValue;
                    Link.GraphicsFormat = oldGraphicsFmt;
                }
                else
                {
                    bool oldUseStdFormValue = Link.TypesetStandardForm;
                    Link.TypesetStandardForm = PictureType != "TraditionalForm";
                    Image = Link.EvaluateToTypeset(mathCommand, Width);
                    Link.TypesetStandardForm = oldUseStdFormValue;
                }
            }
        }
    }


    /// <summary
    /// If a <see cref="MathCommand"/ is being used to create the image to display, this method causes it to
    /// be recomputed to produce a new image.
    /// </summary
    /// <remarks
    /// Call Recompute if your MathCommand depends on values in <iMathematica</i that have changed since
    /// the last time you set the MathCommand property or called Recompute.
    /// </remarks
    /// <seealso cref="MathCommand"/
    /// 
    public void Recompute()
    {
        // Ignore if mathCommand is null (we are in "manual" Image mode).
        if (MathCommand != null)
            MathCommand = MathCommand;
    }

}
*/

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
  private Object procValue;

  private List<double> respSamples;
  private List<double> lastRespSamples;

  private   Boolean isRun;
  protected Thread  autoTuningInThread;



  public AutoTunerPID(ref Object aProcValue)
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

  protected void StartAutoTuning(Object aProcValue /*ref double curValue, double setPoint, double step*/)
  {
    respSamples     = new List<double>();
    lastRespSamples = new List<double>();

    int    rsc;                   //responseSamplesCount / 10
    bool   steadyState = false;
    
    double stepSteadyThr = 0.05;  //threshold on derivative to consider the step response to a steady state

    while (isRun)
    {
      Thread.Sleep(200);  //procValue update   
      Console.WriteLine("What?");
      respSamples.Add((double)aProcValue);  //qf

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
  
  public ConvPID(ref Object aProcValue): base(ref aProcValue) 
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

