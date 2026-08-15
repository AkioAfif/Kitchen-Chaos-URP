using System;
using UnityEngine;

public interface IHasProgress 
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgresChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
}
