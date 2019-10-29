SRCS=Rational.cs \
     ContinuedFraction.cs \
     PeriodicContinuedFractionTerms.cs \
     DoubleContinuedFractionTerms.cs \
     Program.cs

LIBS=-reference:System.Numerics.dll

BIN=Program.exe

all: $(BIN)

$(BIN): $(SRCS)
	csc $(LIBS) $(SRCS)

clean:
	rm -f $(BIN)
