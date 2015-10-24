all:
	g++ -O3 -std=c++11 tests/test_linear_equations.cpp -o tests/test_linear_equations
	g++ -O3 -std=c++11 tests/test_polynomials.cpp -o tests/test_polynomials
	g++ -O3 -std=c++11 tests/test_continuous_colormap.cpp -o tests/test_continuous_colormap
	./tests/test_continuous_colormap
	./tests/test_linear_equations 
	./tests/test_polynomials 
clean:
	rm ./tests/test_continuous_colormap
	rm ./tests/test_linear_equations 
	rm ./tests/test_polynomials 
	rm ./tests/*.html
