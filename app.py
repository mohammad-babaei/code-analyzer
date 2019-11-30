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

    output = None
    if form.validate_on_submit():
        inputText = form.source_code.data
        #run the c# code here
        with open('FormInput.cs', 'w') as f:
            f.write(inputText)

        output = subprocess.check_output(["./c# project/bin/Debug/CocoCompiler2.exe", "FormInput.cs"]).decode("utf-8")
        
        lis = output.split('\r\n')
        paragraphs = []
        for l in lis:
            paragraphs.append("<p>{}</p>".format(l))
        output = " ".join(paragraphs)
    return render_template('index.html', form=form, output = output)

@app.route('/aboutUs', methods=['GET', ])
def about_us():
    return render_template('about_us.html')

@app.route('/this-site', methods=['GET', ])
def about_site():
    return render_template('about_site.html')