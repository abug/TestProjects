using System;
using System.Collections.Generic;
using System.Linq;

namespace EquationCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var inputStr = "2 3 * 4 2 / + 5 3 * 6 + -";

            ICalculator calculator = new Calculator(new SimpleEquationFormatter(), new PostfixEquationCalculator());

            var result = calculator.Calculate(inputStr);

            Console.ReadLine();
        }
    }

    public class PostfixEquationCalculator : IEquationCalculator<decimal>
    {
        private Stack<decimal> _stack;
        private IList<ISymbolHandlers> _handlers;

        public PostfixEquationCalculator()
        {
            _stack = new Stack<decimal>();

            _handlers = new ISymbolHandlers[]
            {
                new NumericHandler(_stack),
                new MultiplyHandler(_stack),
                new DivisionHandler(_stack),
                new PlusHandler(_stack),
                new MinusHandler(_stack)
            };
        }

        public decimal Calculate(string formattedEquation)
        {
            if (formattedEquation == null)
            {
                throw new ArgumentNullException("formattedEquation");
            }

            foreach (var symbol in formattedEquation)
            {
                var handler = _handlers.SingleOrDefault(h => h.CanHandle(symbol));

                if (handler != null)
                {
                    handler.Handle(symbol);
                }
            }

            var result = _stack.Pop();

            return result;
        }
    }

    public class MinusHandler : OperationHandlerBase
    {
        public MinusHandler(Stack<decimal> stack) : base(stack)
        {
        }

        protected override char OperationSymbol
        {
            get { return '-'; }
        }

        protected override decimal Calculate(decimal secondNumber, decimal firstNumber)
        {
            return secondNumber - firstNumber;
        }
    }

    public class PlusHandler : OperationHandlerBase
    {
        public PlusHandler(Stack<decimal> stack)
            : base(stack)
        {
        }

        protected override char OperationSymbol
        {
            get { return '+'; }
        }

        protected override decimal Calculate(decimal secondNumber, decimal firstNumber)
        {
            return secondNumber + firstNumber;
        }
    }

    public class DivisionHandler : OperationHandlerBase
    {
        public DivisionHandler(Stack<decimal> stack)
            : base(stack)
        {
        }

        protected override char OperationSymbol
        {
            get { return '/'; }
        }

        protected override decimal Calculate(decimal secondNumber, decimal firstNumber)
        {
            return secondNumber / firstNumber;
        }
    }

    public abstract class OperationHandlerBase : ISymbolHandlers
    {
        private readonly Stack<decimal> _stack;

        public OperationHandlerBase(Stack<decimal> stack)
        {
            if (stack == null)
            {
                throw new ArgumentNullException("stack");
            }

            _stack = stack;
        }

        protected abstract char OperationSymbol { get; }

        public void Handle(char input)
        {
            var firstNumber = _stack.Pop();

            var secondNumber = _stack.Pop();

            var result = Calculate(secondNumber, firstNumber);

            _stack.Push(result);
        }

        protected abstract decimal Calculate(decimal secondNumber, decimal firstNumber);

        public bool CanHandle(char input)
        {
            var result = input == OperationSymbol;

            return result;
        }
    }

    public class MultiplyHandler : OperationHandlerBase
    {
        public MultiplyHandler(Stack<decimal> stack) :
            base(stack)
        {
        }

        protected override char OperationSymbol
        {
            get { return '*'; }
        }

        protected override decimal Calculate(decimal secondNumber, decimal firstNumber)
        {
            return secondNumber * firstNumber;
        }
    }

    public class NumericHandler : ISymbolHandlers
    {
        private readonly Stack<decimal> _stack;

        public NumericHandler(Stack<decimal> stack)
        {
            _stack = stack;
        }

        public void Handle(char input)
        {
            _stack.Push(decimal.Parse(input.ToString()));
        }

        public bool CanHandle(char input)
        {
            var result = Char.IsNumber(input);

            return result;
        }
    }

    public interface ISymbolHandlers
    {
        void Handle(char input);

        bool CanHandle(char input);
    }

    public class SimpleEquationFormatter : IEquationFormatter
    {
        public string Format(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var result = input.Replace(" ", string.Empty);

            return result;
        }
    }

    public class Calculator : ICalculator
    {
        private IEquationFormatter _equationFormatter;

        private IEquationCalculator<decimal> _equationCalculator;

        public Calculator(
            IEquationFormatter equationFormatter,
            IEquationCalculator<decimal> equationCalculator)
        {
            _equationFormatter = equationFormatter;
            _equationCalculator = equationCalculator;
        }

        public decimal Calculate(string input)
        {
            var formattedEquation = _equationFormatter.Format(input);

            var result = _equationCalculator.Calculate(formattedEquation);

            return result;
        }
    }

    public interface IEquationCalculator<T>
    {
        T Calculate(string formattedEquation);
    }

    public interface IEquationFormatter
    {
        string Format(string input);
    }

    public interface ICalculator
    {
        decimal Calculate(string input);
    }
}
