using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo
{
    interface check_if
    {

        bool check_if_i_can_go_up(int honnan, int meddig);
        bool check_if_i_can_go_down(int honnan, int meddig);
        bool check_if_i_can_go_left(int honnan, int meddig);
        bool check_if_i_can_go_right(int honnan, int meddig);
        int check_ship(string name);


    }
}
