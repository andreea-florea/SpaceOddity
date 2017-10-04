using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class ObservableBlueprintBuilder : IBlueprintBuilder
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
