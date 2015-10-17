from random import randint

def Solve(A, B):
    ''' Linear system solving for AX=B. Returns vector X '''
    N=len(B)
    X=[0]*N
    
    def a(i, j, n):
        if n==N: return A[i][j]
        return a(i,j,n+1)*a(n,n,n+1)-a(i,n,n+1)*a(n,j,n+1)

    def b(i, n):
        if n==N: return B[i]
        return a(n,n,n+1)*b(i,n+1)-a(i,n,n+1)*b(n,n+1)

    def x(i):
        d=b(i,i+1)
        for j in xrange(i): d-=a(i,j,i+1)*X[j]
        return d/a(i,i,i+1)

    for k in xrange(N):
        X[k]=x(k)

    return X
    
    
def Solve2(A, B):
    ''' Linear system solving for AX=B. Returns vector X '''
    N=len(B)
    X=[0]*N
    
    def a(i, j, n, xi):
        if n==N: return A[(i + xi) % N][(j + xi) % N]
        return a(i,j,n+1,xi)*a(n,n,n+1,xi)-a(i,n,n+1,xi)*a(n,j,n+1,xi)

    def b(i, n, xi):
        if n==N: return B[(i + xi) % N]
        return a(n,n,n+1,xi)*b(i,n+1,xi)-a(i,n,n+1,xi)*b(n,n+1,xi)

    def x(i):
        return b(0, 1, i) / a(0, 0, 1, i)

    for k in xrange(N):
        X[k]=x(k)

    return X    
    
def Validate(A, B, X):
	ret = []
	for i in range(3):
		Bi = 0.0
		for j in range(3):
			Bi += A[i][j] * X[j]
		ret = ret + [Bi - B[i]]
	return ret
    
def dot(A, B):
	return sum([ai*bi for (ai, bi) in zip(A, B)])

def scale(A, x):
	return [ai*x for ai in A]

def add(A, B):
	return [ai+bi for (ai, bi) in zip(A, B)]

def project(A, Pn, Pd):
	return add(A, scale(Pn, (Pd - dot(Pn, A)) / dot(Pn, Pn)))

def Solve3(A, B, d=0.00001, i_max=100000):
	n = len(B)
	X = [0.0] * n
	for i in range(i_max):
		for j in range(n):
			X = project(X, A[j], B[j])
		error = sum([abs(ei) for ei in Validate(A, B, X)])
		if error < d:
			print i,
			return X
	print i,
	return X


for n in range(0, 1000):
	A = [[], [], []]
	B = []
	for i in range(0, 3):
		A += []
		for j in range(0, 3):
			A[i] += [randint(-100, 100) / 100.0]
		B += [randint(-100, 100) / 100.0]
	
	X3 = Solve3(A, B)
