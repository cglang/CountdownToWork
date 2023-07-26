import ctypes
import subprocess
import time

import pyautogui
import win32con
import win32gui
import win32process


def enum_windows_proc(hwnd, lparam):
    h_def_view = win32gui.FindWindowEx(hwnd, 0, "SHELLDLL_DefView", None)
    if h_def_view != 0:
        h_workerw = win32gui.FindWindowEx(0, hwnd, "WorkerW", None)
        win32gui.SetWindowPos(lparam, None, 0, 0, 1920, 1080, win32con.SWP_SHOWWINDOW)
        win32gui.SetParent(lparam, h_workerw)
        return False
    return True


if __name__ == '__main__':
    proc = subprocess.Popen(r"wt C:\CountdownToWork.exe", creationflags=subprocess.CREATE_NEW_CONSOLE)
    time.sleep(2)
    pyautogui.hotkey("alt", "enter")
    time.sleep(2)
    user32 = ctypes.windll.user32
    h_wnd = user32.GetForegroundWindow()
    print(h_wnd)
    h_progman = win32gui.FindWindow("Progman", None)
    win32gui.SendMessageTimeout(h_progman, 0x52C, 0, 0, 0, 100)
    win32gui.EnumWindows(enum_windows_proc, h_wnd)
