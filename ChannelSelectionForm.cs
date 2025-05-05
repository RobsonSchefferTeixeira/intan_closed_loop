using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace USB_Scope
{
    public partial class ChannelSelectionForm : Form
    {
        public event Action SomethingHappened;

        public bool channelSelectionFormVisible = false;

        public ChannelSelectionForm()
        {
            InitializeComponent();
        }

        private void ChannelSelectionForm_Load(object sender, EventArgs e)
        {
            this.channelSelectionFormVisible = true;
        }

        public void ChannelSelectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.channelSelectionFormVisible = false;

           if (SomethingHappened != null)
                SomethingHappened();

        }

        private void ChannelSelectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            
            e.Cancel = true; // this cancels the close event.
            this.channelSelectionFormVisible = false;

            if (SomethingHappened != null)
                SomethingHappened();
        }
    }
}
