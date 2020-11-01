using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo
{
    interface i_go
    {
        void i_go_left(int honnan, int meddig);

        void i_go_right(int honnan, int meddig);

        void i_go_up(int honnan, int meddig);

        void i_go_down(int honnan, int meddig);
    }
 }
