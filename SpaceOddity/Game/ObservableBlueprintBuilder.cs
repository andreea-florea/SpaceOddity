using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Interfaces;

namespace Game
{
    public class ObservableBlueprintBuilder : IObservableBlueprintBuilder
    {
        private IBlueprintBuilder baseBuilder;
        private List<IBlueprintBuilderObserver> observers;

        public int Height
        {
            get
            {
                return baseBuilder.Height;
            }
        }

        public int Width
        {
            get
            {
                return baseBuilder.Width;
            }
        }

        public ObservableBlueprintBuilder(IBlueprintBuilder baseBuilder)
        {
            this.baseBuilder = baseBuilder;
            observers = new List<IBlueprintBuilderObserver>();
        }

        #region Block Creation

        public bool CreateBlock(int y, int x)
        {
            var success = baseBuilder.CreateBlock(y, x);
            if (success)
            {
                NotifyObserverOfBlockCreated(y, x);
            }
            else
            {
                NotifyObserverOfErrorWhenCreatingBlock(y, x);
            }

            return success;
        }

        private void NotifyObserverOfBlockCreated(int y, int x)
        {
            foreach (var observer in observers)
            {
                observer.BlockCreated(this, y, x);
            }
        }

        private void NotifyObserverOfErrorWhenCreatingBlock(int y, int x)
        {
            foreach (var observer in observers)
            {
                observer.ErrorBlockNotCreated(this, y, x);
            }
        }

        #endregion

        #region Block Deletion

        public bool DeleteBlock(int y, int x)
        {
            var success = baseBuilder.DeleteBlock(y, x);
            if (success)
            {
                NotifyObserverOfBlockDeleted(y, x);
            }
            else
            {
                NotifyObserverOfErrorWhenDeletingBlock(y, x);
            }

            return success;
        }

        private void NotifyObserverOfErrorWhenDeletingBlock(int y, int x)
        {
            foreach (var observer in observers)
            {
                observer.ErrorBlockNotDeleted(this, y, x);
            }
        }

        private void NotifyObserverOfBlockDeleted(int y, int x)
        {
            foreach (var observer in observers)
            {
                observer.BlockDeleted(this, y, x);
            }
        }
        #endregion

        #region Ship Component Adding

        public bool AddShipComponent(int y, int x, IShipComponent shipComponent)
        {
            var success = baseBuilder.AddShipComponent(y, x, shipComponent);

            if (success)
            {
                NotifyObserverOfShipComponentAdded(y, x);
            }
            else
            {
                NotifyObserverOfErrorWhenAddingShipComponent(y, x);
            }

            return success;
        }

        private void NotifyObserverOfShipComponentAdded(int y, int x)
        {
            foreach (var observer in observers)
            {
                observer.ShipComponentAdded(this, y, x);
            }
        }

        private void NotifyObserverOfErrorWhenAddingShipComponent(int y, int x)
        {
            foreach (var observer in observers)
            {
                observer.ErrorShipComponentNotAdded(this, y, x);
            }
        }
        #endregion

        #region Ship Component Deletion

        public bool DeleteShipComponent(int y, int x)
        {
            var success = baseBuilder.DeleteShipComponent(y, x);

            if (success)
            {
                NotifyObserverOfShipComponentDeleted(y, x);
            }
            else
            {
                NotifyObserverOfErrorWhenDeletingShipComponent(y, x);
            }

            return true;
        }

        private void NotifyObserverOfShipComponentDeleted(int y, int x)
        {
            foreach (var observer in observers)
            {
                observer.ShipComponentDeleted(this, y, x);
            }
        }

        private void NotifyObserverOfErrorWhenDeletingShipComponent(int y, int x)
        {
            foreach (var observer in observers)
            {
                observer.ErrorShipComponentNotDeleted(this, y, x);
            }
        }
        #endregion

        public void AttachObserver(IBlueprintBuilderObserver observer)
        {
            observers.Add(observer);
        }

        public IBlock GetBlock(int y, int x)
        {
            return baseBuilder.GetBlock(y, x);
        }    
    }
}
