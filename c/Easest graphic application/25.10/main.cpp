#include <Windows.h>
#include <tchar.h>

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

TCHAR WinName[] = _T("MainFrame");

int APIENTRY _tWinMain(
	HINSTANCE This, //���������� �������� ����������
	HINSTANCE Prev, //� ����������� �������� 0
	LPTSTR cmd, // ��������� ������
	int mode // ����� ����������� ����
	) { 
	HWND hWnD; //���������� �������� ���� ���������
	MSG msg; //��������� ��� ������� ���������
	WNDCLASS wc; //����� ����

	//��������� ����
	wc.hInstance = This;
	wc.lpszClassName = WinName; //��� ������ ����
	wc.lpfnWndProc = WndProc; //������� ��������� ��������� windows
	wc.style = CS_HREDRAW | CS_VREDRAW; //����� ����
	wc.hIcon = LoadIcon(NULL, IDI_APPLICATION); //����������� ������
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.lpszMenuName = NULL; //��� ����
	wc.cbClsExtra = 0; //��� �������������� ������ ������
	wc.cbWndExtra = 0; //��� �������������� ������ ����
	wc.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);

	//����������� ������ ����
	if (!RegisterClass(&wc)) {
		return 0;
	}

	hWnD = CreateWindow(
		WinName, //��� ������ ����
		_T("Windows-����������"),
		WS_OVERLAPPEDWINDOW, //����� ����
		//���������� ������ �������� ���� ����
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		// ������� ����
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		HWND_DESKTOP, //���������� ������������� ���� (�������� �����)
		NULL, //��� ����
		This, //���������� ����������
		NULL //�������������� ���������� ���
		);

	// �������� ����
	ShowWindow(hWnD, mode);

	//���� ��������� ���������
	while (GetMessage (&msg, NULL, 0, 0)) {
		TranslateMessage(&msg); //������� ���������� ����� ������� �������
		DispatchMessage(&msg); //�������� ��������� ������� WndProc
	}
	return 0;
}

LRESULT CALLBACK WndProc(HWND hWnD, UINT message, WPARAM wParam, LPARAM lParam) {
	//���������� ���������
	switch (message) {
		case WM_DESTROY:
			PostQuitMessage(0);
			break;

		default:
			return DefWindowProc(hWnD, message, wParam, lParam);
	}
}