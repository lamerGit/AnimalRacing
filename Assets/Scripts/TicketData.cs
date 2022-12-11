using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketData 
{
    public TicketType ticketType;
    public int moneyAmount;
    public int first;
    public int second;
    public int third;
    public TicketState ticketState;

    public TicketData()
    {
        this.ticketType = TicketType.None;
        this.moneyAmount = 0;
        this.first = 0;
        this.second = 0;
        this.third = 0;
        this.ticketState = TicketState.None;
    }

    public void TicketInicialize()
    {
        this.ticketType = TicketType.None;
        this.moneyAmount = 0;
        this.first = 0;
        this.second = 0;
        this.third = 0;
        this.ticketState = TicketState.None;
    }
}
