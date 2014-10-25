#include <Windows.h>
#include <tchar.h>

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

TCHAR WinName[] = _T("MainFrame");

int APIENTRY _tWinMain(
	HINSTANCE This, //Дескриптор текущего приложения
	HINSTANCE Prev, //В современных системах 0
	LPTSTR cmd, // Командная строка
	int mode // Режим отображения окна
	) { 
	HWND hWnD; //Дескриптор главного окна программы
	MSG msg; //Структура для хранния сообщения
	WNDCLASS wc; //Класс окна

	//Настройка окна
	wc.hInstance = This;
	wc.lpszClassName = WinName; //Имя класса окна
	wc.lpfnWndProc = WndProc; //Функция обработки сообщений windows
	wc.style = CS_HREDRAW | CS_VREDRAW; //Стиль окна
	wc.hIcon = LoadIcon(NULL, IDI_APPLICATION); //стандартная иконка
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);
	wc.lpszMenuName = NULL; //Нет меню
	wc.cbClsExtra = 0; //Нет дополнительных данных класса
	wc.cbWndExtra = 0; //Нет дополнительных данных окна
	wc.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);

	//Регистрация класса окна
	if (!RegisterClass(&wc)) {
		return 0;
	}

	hWnD = CreateWindow(
		WinName, //Имя класса окна
		_T("Windows-приложение"),
		WS_OVERLAPPEDWINDOW, //Стиль окна
		//Координаты левого верхнего угла окна
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		// Размеры окна
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		HWND_DESKTOP, //Дескриптор родительского окна (рабочего стола)
		NULL, //Нет меню
		This, //Дескриптор приложения
		NULL //Дополнительной инфромации нет
		);

	// Показать окно
	ShowWindow(hWnD, mode);

	//Цикл обработки сообщений
	while (GetMessage (&msg, NULL, 0, 0)) {
		TranslateMessage(&msg); //Функция трансляции кодов нажатой клавиши
		DispatchMessage(&msg); //Посылает сообщение функции WndProc
	}
	return 0;
}

LRESULT CALLBACK WndProc(HWND hWnD, UINT message, WPARAM wParam, LPARAM lParam) {
	//Обработчик сообщений
	switch (message) {
		case WM_DESTROY:
			PostQuitMessage(0);
			break;

		default:
			return DefWindowProc(hWnD, message, wParam, lParam);
	}
}