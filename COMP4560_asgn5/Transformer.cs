﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP4560_asgn5 {
    public partial class Transformer : Form {
        int numpts = 0;
        int numlines = 0;
        bool gooddata = false;
        double[,] vertices;
        double[,] scrnpts;
        double[,] ctrans = new double[4, 4];  //your main transformation matrix
        int[,] lines;

        public Transformer() {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            Text = "COMP 4560:  Assignment 5 (200830) (Your Name Here)";
            ResizeRedraw = true;
            BackColor = Color.Black;
            MenuItem miNewDat = new MenuItem("New &Data...",
                new EventHandler(MenuNewDataOnClick));
            MenuItem miExit = new MenuItem("E&xit",
                new EventHandler(MenuFileExitOnClick));
            MenuItem miDash = new MenuItem("-");
            MenuItem miFile = new MenuItem("&File",
                new MenuItem[] { miNewDat, miDash, miExit });
            MenuItem miAbout = new MenuItem("&About",
                new EventHandler(MenuAboutOnClick));
            Menu = new MainMenu(new MenuItem[] { miFile, miAbout });
        }

        protected override void OnPaint(PaintEventArgs pea) {
            Graphics grfx = pea.Graphics;
            Pen pen = new Pen(Color.White, 3);
            double temp;
            int k;

            if (gooddata) {
                //create the screen coordinates:
                // scrnpts = vertices*ctrans

                for (int i = 0; i < numpts; i++) {
                    for (int j = 0; j < 4; j++) {
                        temp = 0.0d;
                        for (k = 0; k < 4; k++)
                            temp += vertices[i, k] * ctrans[k, j];
                        scrnpts[i, j] = temp;
                    }
                }

                //now draw the lines

                for (int i = 0; i < numlines; i++) {
                    grfx.DrawLine(pen, (int)scrnpts[lines[i, 0], 0], (int)scrnpts[lines[i, 0], 1],
                        (int)scrnpts[lines[i, 1], 0], (int)scrnpts[lines[i, 1], 1]);
                }


            } // end of gooddata block	
        } // end of OnPaint

        void MenuNewDataOnClick(object obj, EventArgs ea) {
            //MessageBox.Show("New Data item clicked.");
            gooddata = GetNewData();
            RestoreInitialImage();
        }

        void MenuFileExitOnClick(object obj, EventArgs ea) {
            Close();
        }

        void MenuAboutOnClick(object obj, EventArgs ea) {
            AboutDialogBox dlg = new AboutDialogBox();
            dlg.ShowDialog();
        }

        void RestoreInitialImage() {
            if (gooddata) {
                BBox bbox = new BBox(vertices);
                Vec3 mid = bbox.getCenter();
            }
            Invalidate();
        } // end of RestoreInitialImage

        bool GetNewData() {
            string strinputfile, text;
            ArrayList coorddata = new ArrayList();
            ArrayList linesdata = new ArrayList();
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.Title = "Choose File with Coordinates of Vertices";
            if (opendlg.ShowDialog() == DialogResult.OK) {
                strinputfile = opendlg.FileName;
                FileInfo coordfile = new FileInfo(strinputfile);
                StreamReader reader = coordfile.OpenText();
                do {
                    text = reader.ReadLine();
                    if (text != null) coorddata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeCoords(coorddata);
            } else {
                MessageBox.Show("***Failed to Open Coordinates File***");
                return false;
            }

            opendlg.Title = "Choose File with Data Specifying Lines";
            if (opendlg.ShowDialog() == DialogResult.OK) {
                strinputfile = opendlg.FileName;
                FileInfo linesfile = new FileInfo(strinputfile);
                StreamReader reader = linesfile.OpenText();
                do {
                    text = reader.ReadLine();
                    if (text != null) linesdata.Add(text);
                } while (text != null);
                reader.Close();
                DecodeLines(linesdata);
            } else {
                MessageBox.Show("***Failed to Open Line Data File***");
                return false;
            }
            scrnpts = new double[numpts, 4];
            setIdentity(ctrans, 4, 4);  //initialize transformation matrix to identity
            return true;
        } // end of GetNewData

        void DecodeCoords(ArrayList coorddata) {
            //this may allocate slightly more rows that necessary
            vertices = new double[coorddata.Count, 4];
            numpts = 0;
            string[] text = null;
            for (int i = 0; i < coorddata.Count; i++) {
                text = coorddata[i].ToString().Split(' ', ',');
                vertices[numpts, 0] = double.Parse(text[0]);
                if (vertices[numpts, 0] < 0.0d) break;
                vertices[numpts, 1] = double.Parse(text[1]);
                vertices[numpts, 2] = double.Parse(text[2]);
                vertices[numpts, 3] = 1.0d;
                numpts++;
            }

        }// end of DecodeCoords

        void DecodeLines(ArrayList linesdata) {
            //this may allocate slightly more rows that necessary
            lines = new int[linesdata.Count, 2];
            numlines = 0;
            string[] text = null;
            for (int i = 0; i < linesdata.Count; i++) {
                text = linesdata[i].ToString().Split(' ', ',');
                lines[numlines, 0] = int.Parse(text[0]);
                if (lines[numlines, 0] < 0) break;
                lines[numlines, 1] = int.Parse(text[1]);
                numlines++;
            }
        } // end of DecodeLines

        void setIdentity(double[,] A, int nrow, int ncol) {
            for (int i = 0; i < nrow; i++) {
                for (int j = 0; j < ncol; j++) A[i, j] = 0.0d;
                A[i, i] = 1.0d;
            }
        }// end of setIdentity


        private void Transformer_Load(object sender, System.EventArgs e) {

        }

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
            if (e.Button == transleftbtn) {
                Refresh();
            }
            if (e.Button == transrightbtn) {
                Refresh();
            }
            if (e.Button == transupbtn) {
                Refresh();
            }

            if (e.Button == transdownbtn) {
                Refresh();
            }
            if (e.Button == scaleupbtn) {
                Refresh();
            }
            if (e.Button == scaledownbtn) {
                Refresh();
            }
            if (e.Button == rotxby1btn) {

            }
            if (e.Button == rotyby1btn) {

            }
            if (e.Button == rotzby1btn) {

            }

            if (e.Button == rotxbtn) {

            }
            if (e.Button == rotybtn) {

            }

            if (e.Button == rotzbtn) {

            }

            if (e.Button == shearleftbtn) {
                Refresh();
            }

            if (e.Button == shearrightbtn) {
                Refresh();
            }

            if (e.Button == resetbtn) {
                RestoreInitialImage();
            }

            if (e.Button == exitbtn) {
                Close();
            }

        }
    }
}
