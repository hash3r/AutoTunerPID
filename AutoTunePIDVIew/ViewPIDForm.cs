/*  PID Loop Control example.  By Lowell Cady, LowTech LLC (c) 2009
 *  
 * Distributed under "The Code Project Open License (CPOL) 1.02"
 * http://www.codeproject.com/info/CPOL.zip
 * 
 * Modify and/or re-apply code at your own risk.
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ViewPID
{
    public partial class ViewPIDForm : Form
    {
        private ConvPID conv;
        Object obPV;  

        private double pSetpoint = 0;
        private double pPV = 0;  // actual possition (Process Value)
        private double pError = 0;   // how much SP and PV are diff (SP - PV)
        private double pIntegral = 0; // curIntegral + (error * Delta Time)
        private double pDerivative = 0;  //(error - prev error) / Delta time
        private double pPreError = 0; // error from last time (previous Error)
        private double pKp = 0.2, pKi = 0.01, pKd = 1; // PID constant multipliers
        private double pDt = 100.0; // delta time
        private double pOutput = 0; // the drive amount that effects the PV.
        private double pNoisePercent = 0.02; // amount of the full range to randomly alter the PV
        private StripChart stripChart;  //builds and contains the strip chart bmp
        private double pNoise = 0;  // random noise added to PV

        public double setpoint  //SP, the value desired from the process (i.e. desired temperature of a heater.)
        {
            get { return pSetpoint; }
            set { 
                    pSetpoint = value;
                    // change the label for setpoint value
                    lblSP.Text = pSetpoint.ToString("F");
                    // change the slider possition if it does not match
                    if ( (int)Math.Round(pSetpoint) != trackBarSP.Value) // don't use doubles in == or != expressions due to bit resolution
                        trackBarSP.Value = (int)Math.Round(pSetpoint);
                }
        }
        public double PV    //Process Value, the measured value of the process (i.e. actual temperature of a heater)
        {
            get { return pPV; }
            set {
                    pPV = value;
                    obPV = value;
                    // place limits on the measured value
                    if (pPV < 0)
                        pPV = 0;
                    else if (pPV > 1000)
                        pPV = 1000;
                    // update the text
                    lblPV.Text = pPV.ToString("F");
                    // update progress bar
                    if (pPV > progBarPV.Maximum)
                        progBarPV.Value = progBarPV.Maximum;
                    else if (pPV < progBarPV.Minimum)
                        progBarPV.Value = progBarPV.Minimum;
                    else
                        progBarPV.Value = (int)pPV;

                }
        }
        public double error //difference between Setpoint and Process value (SP - PV)
        {
            get {return pError; }
            set 
            { 
                pError = value;
                // update the lable
                lblError.Text = pError.ToString("F");
            }
        }
        public double integral //sum of recent errors
        {
            get {return pIntegral;}
            set 
            { 
                pIntegral = value;
                // update the lable
                lblIntegral.Text = integral.ToString("F");
            }
        }
        public double derivative    //How much the error is changing (the slope of the change)
        { 
            get {return pDerivative;} 
            set 
            { 
                pDerivative = value;
                //Math.Round(pDerivative, 2);
                // update the lable
                lblDeriv.Text = derivative.ToString("F");
            } 
        }
        public double preError  //Previous error, the error last time the process was checked.
        {
            get 
            { 
              return pPreError; 
            }
            set { pPreError = value; }
        }
        public double Kp    //proportional gain, a "constant" the error is multiplied by. Partly contributes to the output as (Kp * error)
        {
            get { return pKp; }
            set 
            { 
                pKp = value;
                // update the textBox
                if (pKp.CompareTo(validateDouble(tbKp.Text, pKp)) != 0)
                    tbKp.Text = pKp.ToString();

            }
        }
        public double Ki    // integral gain, a "constant" the sum of errors will be multiplied by.
        {
            get { return pKi; }
            set 
            { 
                pKi = value;
                // update the textBox
                if (pKi.CompareTo(validateDouble(tbKi.Text, pKi)) != 0)
                    tbKi.Text = pKi.ToString();

            }
        }
        public double Kd    // derivative gain, a "constant" the rate of change will be multiplied by.
        {
            get { return pKd; }
            set 
            { 
                pKd = value;
                // update the textBox
                if (pKd.CompareTo(validateDouble(tbKd.Text, pKd)) != 0)
                    tbKd.Text = pKd.ToString();
            }
        }
        public double Dt    // delta time, the interval between saples (in milliseconds).
        {
            get { return pDt; }
            set { pDt = value; }
        }
        public double output    //the output of the process, the value driving the system/equipment.  (i.e. the amount of electricity supplied to a heater.)
        {
            get { return pOutput; }
            set 
            { 
                pOutput = value; 
                // limit the output
                if (pOutput < 0)
                    pOutput = 0;
                else if (pOutput > 1000)
                    pOutput = 1000;

                if (pOutput > progBarOut.Maximum)
                    progBarOut.Value = progBarOut.Maximum;
                else if (pOutput < progBarOut.Minimum)
                    progBarOut.Value = progBarOut.Minimum;
                else
                    progBarOut.Value = (int)pOutput;

              lblOutput.Text = pOutput.ToString("F");
            }
        }
        public double noisePercent  //upper limit to the amount of artificial noise (random distortion) to add to the PV (measured value).  0.0 to 1.0 (0 to 100%) 
        { 
            get { return pNoisePercent;}
            set { pNoisePercent = value; }
        }

        public double noise     //amount of random noise added to the process value
        {
            get { return pNoise; }
            set { pNoise = value; }
        }

        // form constructor
        public ViewPIDForm()
        {
            InitializeComponent();

            stripChart = new StripChart();

        }
        
        // set up some of the labels when the form loads.
        private void Form1_Load(object sender, EventArgs e)
        {
            lblInterval.Text = trackBarInterval.Value.ToString();
            lblSP.Text = trackBarSP.Value.ToString();
        }

        // set the sample rate
        private void trackBarInterval_Scroll(object sender, EventArgs e)
        {
            lblInterval.Text = trackBarInterval.Value.ToString();
            tmrPID_Ctrl.Enabled = false;
            tmrPID_Ctrl.Interval = trackBarInterval.Value;
            Dt = tmrPID_Ctrl.Interval;
            tmrPID_Ctrl.Enabled = true;
        }
                
        /*This represents the speed at which electronics could actualy 
            sample the process values.. and do any work on them.
         * [most industrial batching processes are SLOW, on the order of minutes.
         *  but were going to deal in times 10ms to 1 second.  
         *  Most PLC's have relativly slow data busses, and would sample
         *  temperatures on the order of 100's of milliseconds. So our 
         *  starting time interval is 100ms]
         */
        private void tmrPID_Ctrl_Tick(object sender, EventArgs e)
        {   /*
             * Pseudocode from Wikipedia
             * 
                previous_error = 0
                integral = 0 
            start:
                error = setpoint - PV(actual_position)
                integral = integral + error*dt
                derivative = (error - previous_error)/dt
                output = Kp*error + Ki*integral + Kd*derivative
                previous_error = error
                wait(dt)
                goto start
             */
            // calculate the difference between the desired value and the actual value
            error = setpoint - PV;  
            // track error over time, scaled to the timer interval
            integral = integral + (error * Dt);
            // determin the amount of change from the last time checked
            derivative = (error - preError) / Dt; 

            // calculate how much drive the output in order to get to the 
            // desired setpoint. 
            output = (Kp * error) + (Ki * integral) + (Kd * derivative);

            // remember the error for the next time around.
            preError = error;             
            
        }

        //This timer updates the process data. it needs to be the fastest interval in the system.
        private void tmrPV_Tick(object sender, EventArgs e)
        {
            /* this my version of cruise control. 
             * PV = PV + (output * .2) - (PV*.10);
             * The equation contains values for speed, efficiency,
             *  and wind resistance.
               Here 'PV' is the speed of the car.
               'output' is the amount of gas supplied to the engine.
             * (It is only 20% efficient in this example)
               And it looses energy at 10% of the speed. (The faster the 
               car moves the more PV will be reduced.)
             * Noise is added randomly if checked, otherwise noise is 0.0
             * (Noise doesn't relate to the cruise control, but can be useful
             *  when modeling other processes.)
             */
            PV = PV + (output * 0.1) - (PV * 0.5) + noise;
            // change the above equation to fit your aplication
        }

        //change the setpoint
        private void trackBarSP_Scroll(object sender, EventArgs e)
        {
            setpoint = trackBarSP.Value;
        }

        // change a double only if the string can be parsed as a double
        private double validateDouble(string text,double startVal)
        {
            double d;
            if (double.TryParse(text, out d))
                return d;
            else
                return startVal;            
        }

        // Change the Proportional gain text, so validate it.
        private void tbKp_TextChanged(object sender, EventArgs e)
        {
            Kp = validateDouble(tbKp.Text, Kp); 
        }

        // Change the Integral gain text, so validate it.
        private void tbKi_TextChanged(object sender, EventArgs e)
        {
            Ki = validateDouble(tbKi.Text, Ki);
        }

        // Change the Derivative gain text, so validate it.
        private void tbKd_TextChanged(object sender, EventArgs e)
        {            
            Kd = validateDouble(tbKd.Text, Kd);
        }

        // change the amount of noise introduced every tmrPV data update
        private void nudNoisePercent_ValueChanged(object sender, EventArgs e)
        {
            noisePercent = (double)(nudNoisePercent.Value) / 100.0;
        }

        // allow the whole process to be toggled on and off (in case you see something interesting in the chart.)
        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            tmrPV.Enabled = !tmrPV.Enabled;
            tmrPID_Ctrl.Enabled = tmrPV.Enabled;
            tmrChart.Enabled = tmrPV.Enabled;

            if (tmrPV.Enabled)
                btnStart.Text = "Stop Process";
            else
                btnStart.Text = "Start Process";
        }

        // The chart requires more resources when updated.
        // so the update chart happens at a slower rate than the data update (tmrPV is the data update)
        // However, setting the chart update too slow (more than 3x the data update)
        // will cause resolution loss in the graph (the graph will look blurry).
        private void tmrChart_Tick(object sender, EventArgs e)
        {
            // update the stripchart
            stripChart.addSample(setpoint, PV, output);
            // update will rebuild the chart bitmap.
            // put the new chart bitmap in the pictureBox
            pictureBox1.Image = stripChart.bmp;
        }

        private void tmrNoise_Tick(object sender, EventArgs e)
        {

            // noise is added if the checkBox has been clicked
            Random r = new Random();
            if (cbNoise.Checked)
                noise = (progBarPV.Maximum * noisePercent) * (r.NextDouble() - 0.5);
            else
                noise = 0;
            // add a positive or negative noise
            // first get the max allowable noise, then multiply by a random value between -0.5 and 0.5
            /*[The noise doesn't really represent what happens to a car
             * in the real world, but it is usefull if you model other processes.
             * This part of the code into its own timer in order to have much more
             * control over the noise frequency.
            */
        }

        private void StartAutoTunerPID(object sender, MouseEventArgs e)
        {
         // obPV = pPV;
          conv = new ConvPID(ref obPV);
          setpoint += 190;
        }

        


    }

    /* this class will build a strip chart bitmap from consecutive
     *  SP and PV values.  For simplicity, the limits are fixed.
     */
    public class StripChart
    {
        private Bitmap pBmp;    // the chart bitmap
        private Queue<double> qSP;  //collection of Setpoints
        private Queue<double> qPV;  //collection of Precess Values
        private Queue<double> qMV;  //collection of Manipulated values (outputs)

        private int x = 0, y = 0; // used to define points added to line array

        // define brushes 
        private SolidBrush brSP;    // setpoint brush
        private SolidBrush brPV;    // process value brush
        private SolidBrush brMV;    // Manipulated Value brush

        // define pens 
        private Pen pSP;    
        private Pen pPV;
        private Pen pMV;
        

        public Bitmap bmp   // allow the bitmap to be accessed, but not mutated publicly.
        {
            get { return pBmp; }
        }


        public StripChart()
        {
            // define the bitmap.
            pBmp = new Bitmap(1007, 1007, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            // instatiate the queues
            qSP = new Queue<double>(1000);
            qPV = new Queue<double>(1000);
            qMV = new Queue<double>(1000);

            // make brushes that are lightly transparent
            brSP = new SolidBrush(Color.FromArgb(255, Color.Green));
            brPV = new SolidBrush(Color.FromArgb(128, Color.Blue));
            brMV = new SolidBrush(Color.FromArgb(128, Color.Red));

            // make pens that will be used to draw the lines
            pSP = new Pen(brSP, 5);
            pPV = new Pen(brPV, 5);
            pMV = new Pen(brMV, 5);

        }

        //enter sampled values into the associated queues
        public void addSample(double sp, double pv, double mv)
        {
            // limit the size to 1000, if more than 1000 remove the first item
            if (qSP.Count > 999)
                qSP.Dequeue();
            if (qPV.Count > 999)
                qPV.Dequeue();
            if (qMV.Count > 999)
                qMV.Dequeue();

            // place values into the queues
            qSP.Enqueue(sp);
            qPV.Enqueue(pv);
            qMV.Enqueue(mv);
           
            // update the bitmap.
            buildChart();
            
        }

        // empty out the queues
        public void clearQueus()
        {
            qSP.Clear();
            qPV.Clear();
            qMV.Clear();
        }

        public void buildChart()
        {
            Graphics g = Graphics.FromImage(pBmp);
            
            // clear the bmp
            g.Clear(Color.LightGray);
            
            // make point arrays for the lines that will be drawn
            Point[] pointSP = new Point[qSP.Count];
            Point[] pointPV = new Point[qPV.Count];
            Point[] pointMV = new Point[qMV.Count];


            for (x = 0; x < qSP.Count; x++)
            {
                y = 1000 - (int)Math.Round(qSP.ElementAt(x));
                //if (y > 1000) y = 1000; // prevent the line from falling out of image.
                pointSP[x] = new Point(x, y);

                y = 1000 - (int)Math.Round(qPV.ElementAt(x));
                //if (y > 993) y = 993; // prevent the line from falling out of image.
                pointPV[x] = new Point(x, y);

                y = 1000 - (int)Math.Round(qMV.ElementAt(x));
                //if (y > 993) y = 993; // prevent the line from falling out of image.
                pointMV[x] = new Point(x, y);


            }

            /*
            foreach (double d in qSP)
            {
                y =  1000 - (int)Math.Round(d);
                //gPath.AddRectangle(new Rectangle(x,y,4,4));
                pointSP[x] = new Point(x, y);
                x++;
             }
            */


            if (pointSP.Length > 1)
            {
                //g.DrawPath(Pens.Red, gPath);
                g.DrawLines(pSP, pointSP);
                g.DrawLines(pPV, pointPV);
                g.DrawLines(pMV, pointMV);
            }
            

            g.Dispose();
        }


    }



}

