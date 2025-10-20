void arrsum(int n, int arr[], int *sump) {
  int i;
  int s;
  i = 0;
  s = 0;
  while (i < n) {
    s = s + arr[i];
    i = i + 1;
    }
    *sump = s;
}


/* void main() {
  int a[4];
  int s;
  a[0] = 7;
  a[1] = 13;
  a[2] = 9;
  a[3] = 8;
  arrsum(4, a, &s);
  print s;
  println;
} */

void squares(int n, int arr[]) {
    int i;
    i = 0;

    while (i < n) {
        arr[i] = i * i;
        i = i + 1;
    }
}

/* void main(int n) {
    int a[20];
    int s;
    if (n > 20) {
        print 0; //indicates error 
    } else {
        squares(n, a);
        arrsum(n, a, &s);
        print s; 
        println;
    }
} */   

void histogram(int n, int ns[], int max, int freq[]) {
    int i;
    i = 0;
    while (i < n) {
        if (ns[i] <= max) {
            freq[ns[i]] = freq[ns[i]] + 1;
        }
        i = i + 1;
    }
}

void main() {
    int arr[7];
    int f[4];
    f[0] = 0; f[1] = 0; f[2] = 0; f[3] = 0;
    arr[0] = 1; arr[1] = 2; arr[2] = 1; arr[3] = 1; arr[4] = 1;
    arr[5] = 2; arr[6] = 0;
    histogram(7, arr, 3, f);
    print f[0]; print f[1]; print f[2]; print f[3]; 
    println;
}