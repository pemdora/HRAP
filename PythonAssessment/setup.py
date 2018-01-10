import os.path
import sys
import cx_Freeze
import matplotlib
from cx_Freeze import setup, Executable

# PYTHON_INSTALL_DIR = os.path.dirname(os.path.dirname(os.__file__))
# os.environ['TCL_LIBRARY'] = os.path.join(PYTHON_INSTALL_DIR, 'tcl', 'tcl8.6')
# os.environ['TK_LIBRARY'] = os.path.join(PYTHON_INSTALL_DIR, 'tcl', 'tk8.6')

os.environ['TCL_LIBRARY'] = r'C:\Users\Tara\AppData\Local\Programs\Python\Python36-32\tcl\tcl8.6'
os.environ['TK_LIBRARY'] = r'C:\Users\TARA\AppData\Local\Programs\Python\Python36-32\tcl\tk8.6'

base = 'Win32GUI' if sys.platform == 'win32' else None

build_exe_options = {"includes":["matplotlib.backends.backend_tkagg"],"packages": ['matplotlib.backends.backend_tkagg',"tkinter","numpy.lib.format","sklearn","pandas","numpy.core._methods","scipy"],
                     "include_files":[(matplotlib.get_data_path(), "mpl-data"),r"C:\Users\TARA\AppData\Local\Programs\Python\Python36-32\DLLs\tcl86t.dll",
                 r"C:\Users\TARA\AppData\Local\Programs\Python\Python36-32\DLLs\tk86t.dll"],}
# On appelle la fonction setup
setup(
    name = "TEST",
    options = {"build_exe": build_exe_options},
    version = "1",
    description = "Votre programme",
    executables = [Executable("job_assessment.py")],
)
