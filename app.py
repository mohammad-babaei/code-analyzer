from flask import Flask, render_template
from flask_wtf import FlaskForm
from flask_codemirror.fields import CodeMirrorField
from wtforms.fields import SubmitField
from flask_codemirror import CodeMirror
from flask_bootstrap import Bootstrap
import subprocess

SECRET_KEY = 'secret!'
CODEMIRROR_LANGUAGES = ['clike', 'html']
CODEMIRROR_THEME = 'mdn-like'
CODEMIRROR_ADDONS = (
     ('display','placeholder'),
)

app = Flask(__name__)
app.config.from_object(__name__)
codemirror = CodeMirror(app)
Bootstrap(app)

class MyForm(FlaskForm):
    source_code = CodeMirrorField(language='clike',
                                config={'lineNumbers' : 'true'})
    submit = SubmitField('Submit')

@app.route('/', methods = ['GET', 'POST'])
def index():
    form = MyForm()
    sample = open('sampleToAnalize.cs', 'r')
    form.source_code.data = sample.read()

    ListOfTokens = None
    LongMethods = None
    MethodsExecessiveParameters = None
    NoneEndglishID = None
    if form.validate_on_submit():
        inputText = form.source_code.data
        #run the c# code here
        # with open('FormInput.cs', 'w') as f:
        #     f.write(inputText)

        # output = subprocess.check_output(["./c# project/bin/Debug/CocoCompiler2.exe", "FormInput.cs"]).decode("utf-8")
        
        # lis = output.split('\r\n')
        # paragraphs = []
        # for l in lis:
        #     paragraphs.append("<p>{}</p>".format(l))
        # output = " ".join(paragraphs)
        ListOfTokens = 'SELECT action.descr as "action", <br>'\
                    'role.id as role_id,<br>'\
                    'role.descr as role<br>'\
                    'FROM <br>'\
                    'public.role_action_def,<br>'\
                    'public.role,<br>'\
                    'public.record_def, <br>'\
                    'public.action<br>'\
                    'WHERE role.id = role_action_def.role_id AND<br>'\
                    'record_def.id = role_action_def.def_id AND<br>'
        
        LongMethods = 'SELECT action.descr as "action", <br>'\
                    'role.id as role_id,<br>'\
                    'role.descr as role<br>'\
                    'FROM <br>'\
                    'public.role_action_def,<br>'\
                    'public.role,<br>'\
                    'public.record_def, <br>'\
                    'public.action<br>'\
                    'WHERE role.id = role_action_def.role_id AND<br>'\
                    'record_def.id = role_action_def.def_id AND<br>'

        MethodsExecessiveParameters = 'SELECT action.descr as "action", <br>'\
                    'role.id as role_id,<br>'\
                    'role.descr as role<br>'\
                    'FROM <br>'\
                    'public.role_action_def,<br>'\
                    'public.role,<br>'\
                    'public.record_def, <br>'\
                    'public.action<br>'\
                    'WHERE role.id = role_action_def.role_id AND<br>'\
                    'record_def.id = role_action_def.def_id AND<br>'

        NoneEndglishID = 'SELECT action.descr as "action", <br>'\
                    'role.id as role_id,<br>'\
                    'role.descr as role<br>'\
                    'FROM <br>'\
                    'public.role_action_def,<br>'\
                    'public.role,<br>'\
                    'public.record_def, <br>'\
                    'public.action<br>'\
                    'WHERE role.id = role_action_def.role_id AND<br>'\
                    'record_def.id = role_action_def.def_id AND<br>'
    return render_template('index.html', form=form, ListOfTokens = ListOfTokens, LongMethods = LongMethods, MethodsExecessiveParameters = MethodsExecessiveParameters, NoneEndglishID=NoneEndglishID)

@app.route('/aboutUs', methods=['GET', ])
def about_us():
    return render_template('about_us.html')

@app.route('/this-site', methods=['GET', ])
def about_site():
    return render_template('about_site.html')